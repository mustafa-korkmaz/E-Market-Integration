SELECT 
    c.category_id AS CategoryId,
    cd.name AS CategoryName,
    c.parent_id AS ParentCategoryId,
    IFNULL(cd1.name,'') AS ParentCategoryName
FROM
    category c
        LEFT JOIN
    category_description cd ON (c.category_id = cd.category_id)
        LEFT JOIN
    category_description cd1 ON (c.parent_id = cd1.category_id)
