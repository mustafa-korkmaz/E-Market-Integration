<?php
class ModelExportCategory extends Model {
	function getExtensions($type) {
		$query = $this->db->query("SELECT * FROM " . DB_PREFIX . "extension WHERE `type` = '" . $this->db->escape($type) . "'");

		return $query->rows;
	}
	
	function getCategories(){ 
		$query = $this->db->query("SELECT c.category_id AS CategoryId, cd.name AS CategoryName, c.parent_id AS ParentCategoryId,
								   IFNULL(cd1.name,'') AS ParentCategoryName FROM " . DB_PREFIX . "category c 
								   LEFT JOIN " . DB_PREFIX . "category_description cd ON (c.category_id = cd.category_id)
								   LEFT JOIN " . DB_PREFIX . "category_description cd1 ON (c.parent_id = cd1.category_id)");

		return $query->rows;
	
	return $myArray;
	}
}