<?php  
class ControllerExportCategory extends Controller {
public function index(){
	$this->load->model('export/category');
	
	$categories = $this->model_export_category->getCategories();
	
	header('Content-Type: application/json');
	echo(json_encode($categories));
	}
}
?>