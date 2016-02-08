<?php
class ModelExportProduct extends Model {
	
	function getProducts(){ 

	$this->createTempProductToCategoryTable();
	
	$query = $this->db->query("SELECT p.product_id AS ProductId, p.model AS ProductCode, pd.name AS ProductName, 
					     	  pd.description AS ProductDescription, p.status AS Status, cd.category_id AS CategoryId,cd.name AS CategoryName,
							  m.manufacturer_id AS BrandId, m.name AS BrandName, p.quantity AS Quantity, p.price AS Price,
							  IFNULL((tra.rate),0) AS TaxRate, 0 AS TaxIncluded,
							  IFNULL((SELECT ps1.price
										FROM " . DB_PREFIX ."product_special ps1
										WHERE
											ps1.product_id = p.product_id
										ORDER BY ps1.priority
										LIMIT 1),
									0) AS SpecialPrice,
							  p.image AS DefaultImage
							  FROM " . DB_PREFIX . "product p
							  JOIN temp_product_to_category temp_p2c ON (p.product_id = temp_p2c.product_id)
							  JOIN " . DB_PREFIX . "category_description cd ON (cd.category_id = temp_p2c.category_id)
							  JOIN " . DB_PREFIX . "manufacturer m ON (m.manufacturer_id = p.manufacturer_id)
							  JOIN " . DB_PREFIX . "product_description pd ON (pd.product_id = p.product_id)
							  LEFT JOIN " . DB_PREFIX . "tax_rule tru ON (p.tax_class_id = tru.tax_class_id)
							  LEFT JOIN " . DB_PREFIX . "tax_rate tra ON (tra.tax_rate_id = tru.tax_rate_id)");

	$this->dropTempProductToCategoryTable();
		
	return $query->rows;
	}
	
	function getProductAttributes(){ 
	$query = $this->db->query("SELECT pa.product_id AS ProductId, ad.attribute_id AS AttributeId,
							  ad.name AS AtrributeName, pa.text AS AttributeValue
							  FROM " . DB_PREFIX . "product_attribute pa
							  JOIN " . DB_PREFIX . "attribute_description ad ON (pa.attribute_id = ad.attribute_id)");
		return $query->rows;
	}
	
	function getProductImages(){ 
	$query = $this->db->query("SELECT product_id AS ProductId, image AS Image
							  FROM " . DB_PREFIX . "product_image
							  ORDER BY product_id");
		return $query->rows;
	}
	
	function getProductOptions(){ 
	$query = $this->db->query("SELECT po.product_id AS ProductId, od.option_id AS OptionId, od.name AS OptionName, ovd.name AS OptionValue
							  FROM " . DB_PREFIX . "product_option po
							  JOIN " . DB_PREFIX . "option_description od ON (po.option_id = od.option_id)
							  JOIN " . DB_PREFIX . "option_value_description ovd on(ovd.option_id=po.option_id)
							  ORDER BY po.product_id, po.option_id");

		return $query->rows;
	}
	
	function getProductShippingMethods(){ 
	$query = $this->db->query("SELECT ex.extension_id AS FirmId, ex.code AS FirmName, s.value AS Cost
							   FROM " . DB_PREFIX . "extension ex
							   JOIN " . DB_PREFIX . "setting s ON (s.code = ex.code)
							   WHERE s.store_id = 0 AND ex.type = 'shipping'
							   AND (s.key = CONCAT(ex.code, '_cost')
							   OR s.key = CONCAT(ex.code, '_total'))");

		return $query->rows;
	}
	
	function createTempProductToCategoryTable(){
	
	$command = $this->db->query("CREATE TEMPORARY TABLE IF NOT EXISTS temp_product_to_category (product_id int,category_id int);");
	$command = $this->db->query("TRUNCATE TABLE temp_product_to_category;");
	$command = $this->db->query("INSERT INTO temp_product_to_category 
					 SELECT pc.product_id,max(pc.category_id) 
					 FROM " . DB_PREFIX . "product_to_category pc 
				     GROUP BY pc.product_id;");	
	return null;
	}
	
	function dropTempProductToCategoryTable(){
	
	$command = $this->db->query("DROP TABLE IF EXISTS temp_product_to_category;");
	return null;
	}
}	
