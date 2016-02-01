SELECT 
    pa.product_id AS ProductId,
    ad.attribute_id AS AttributeId,
    ad.name AS AtrributeName,
    pa.text AS AttributeValue
FROM
    product_attribute pa
        JOIN
    attribute_description ad ON (pa.attribute_id = ad.attribute_id)