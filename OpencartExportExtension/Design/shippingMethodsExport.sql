SELECT 
    ex.extension_id AS FirmId,
    ex.code AS FirmName,
    s.value AS Cost
FROM
    Extension ex
        JOIN
    setting s ON (s.code = ex.code)
WHERE
    s.store_id = 0 AND ex.type = 'shipping'
        AND (s.key = CONCAT(ex.code, '_cost')
        OR s.key = CONCAT(ex.code, '_total'));
        