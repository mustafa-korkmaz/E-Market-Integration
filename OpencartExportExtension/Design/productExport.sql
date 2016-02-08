CREATE TEMPORARY TABLE IF NOT EXISTS temp_product_to_category (product_id int,category_id int); 

TRUNCATE TABLE temp_product_to_category;

INSERT INTO temp_product_to_category

 SELECT pc.product_id,max(pc.category_id) FROM product_to_category pc
 GROUP BY pc.product_id; -- category - product 1-1 matching 

 
SELECT 
    p.product_id AS ProductId,
    p.model AS ProductCode,
    pd.name AS ProductName,
    pd.description AS ProductDescription,
    p.status AS Status,
    cd.category_id AS CategoryId,cd.name AS CategoryName,
    m.manufacturer_id AS BrandId,
    m.name AS BrandName,
    p.quantity AS Quantity,
    p.price AS Price,
    IFNULL((tra.rate),0) AS TaxRate,
    0 AS TaxIncluded,
    IFNULL((SELECT 
                    ps1.price
                FROM
                    product_special ps1
                WHERE
                    ps1.product_id = p.product_id
                ORDER BY ps1.priority
                LIMIT 1),
            0) AS SpecialPrice,
    p.image AS DefaultImage
FROM
    product p
        JOIN
    temp_product_to_category temp_p2c ON (p.product_id = temp_p2c.product_id)
        JOIN
    category_description cd ON (cd.category_id = temp_p2c.category_id)
        JOIN
    manufacturer m ON (m.manufacturer_id = p.manufacturer_id)
        JOIN
    product_description pd ON (pd.product_id = p.product_id)
        LEFT JOIN
    tax_rule tru ON (p.tax_class_id = tru.tax_class_id)
        LEFT JOIN
    tax_rate tra ON (tra.tax_rate_id = tru.tax_rate_id);


DROP TABLE IF EXISTS temp_product_to_category; 
