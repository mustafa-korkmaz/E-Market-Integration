<?php  
class ControllerExportProduct extends Controller {
public function index(){
	$this->load->model('export/product');
	
	$products = $this->model_export_product->getProducts();
	
	$productAttributes = $this->model_export_product->getProductAttributes();
	$productImages = $this->model_export_product->getProductImages();
	$productOptions = $this->model_export_product->getProductOptions();
	$productShippingMethods = $this->model_export_product->getProductShippingMethods();
	
	$productArray=array("Products" => $products, "ProductAttributes" => $productAttributes,
						"ProductImages" => $productImages,"ProductOptions" => $productOptions,
						"ProductShippingMethods" => $productShippingMethods
						);
	
	header('Content-Type: application/json');
	echo(json_encode($productArray));
	
	}
}
?>