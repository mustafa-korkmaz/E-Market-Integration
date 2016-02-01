SELECT 
    po.product_id AS ProductId,
    od.option_id AS OptionId,
    od.name AS OptionName,
    ovd.name AS OptionValue
FROM
    product_option po
        JOIN
    option_description od ON (po.option_id = od.option_id)
    JOIN option_value_description ovd on(ovd.option_id=po.option_id)
    
    order by po.product_id,po.option_id