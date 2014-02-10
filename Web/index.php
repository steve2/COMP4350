<!DOCTYPE html>
<html>
<head>
	<title>COMP 4350 EC2 Server</title>
	<link rel="stylesheet" href="/css/style.css">
	<link rel="stylesheet" href="/css/smoothness/jquery-ui-1.10.4.custom.css">
	<script src="/jquery/jquery-1.10.2.js"></script>
	<script src="/jquery/jquery-ui-1.10.4.custom.js"></script>
	
	<script>
		$(function() { $("#tabs").tabs(); });
	</script>
</head>

<body>
	
	<div class="titlebar">
		<h1>EC2 Web Page</h1> 
	</div>
	
	<div id="tabs">
		<ul>
			<li><a href="#tabs-0">Home</a></li>
			<li><a href="#tabs-1">Account</a></li>
			<li><a href="#tabs-2">Items</a></li>
			<li><a href="#tabs-3">Marketplace</a></li>
		</ul>
		<div id="tabs-0">
			Home Tab. News?
		</div>
		<div id="tabs-1">
			Account Information / Sign In / Registration
		</div>
		<div id="tabs-2">
			Item Information <br>
			
			<div>
			<?php
				$conn = mysqli_connect("localhost", "COMP4350_admin", "admin", "COMP4350_GRP5");
				if (mysqli_connect_errno())
					print 'Failed to connect to local DB \"COMP4350_admin\".';
					
				$query = "SELECT Item.ID AS itemID, Item.Name AS itemName, Item.Description AS itemDescr, Item_Attributes.Item_ID AS itemID, Item_Attributes.Attribute_ID AS attrID, Item_Attributes.Value AS attrValue, Attribute.ID AS attrID, Attribute.Text AS attrText FROM Item JOIN Item_Attributes ON Item.ID=Item_Attributes.Item_ID JOIN Attribute ON Attribute.ID=Item_Attributes.Attribute_ID";
				$result = mysqli_query($conn, $query);
				
				$previd = NULL;
				while ($row = mysqli_fetch_array($result)) {				
					if ($previd == NULL) {
						echo "<p>";
						echo "<strong>".$row['itemName']."</strong><br>";
						echo $row['itemDescr']."<br>";
						echo "ID: #".$row['itemID']."<br>";
					}
					if ($previd != NULL && $previd != $row['itemID']) {
						echo "</p><p>";
						echo "<strong>".$row['itemName']."</strong><br>";
						echo $row['itemDescr']."<br>";
						echo "ID: #".$row['itemID']."<br>";
					}
					echo $row['attrText']." (".$row['attrValue'].")<br>";
					$previd = $row['itemID'];
				} 
				echo "</p>";
				mysqli_close($conn);
			 ?>
			</div>
			
		</div>
		<div id="tabs-3" class="tab-content">
			Marketplace - Trade/Craft/Buy Items
		</div>
	</div>
	
	<div id="toolbar-container">
		<div id="toolbar" class="ui-widget-header ui-corner-all"/>
	</div>

	<script>
		
		function updateClock() {				
			var splt = Date().split(" ");
			document.getElementById('toolbar').innerHTML 
					= splt[4] + ' ' + splt[1] + ' ' + splt[2];
			setTimeout(updateClock, 1000);
		}
		updateClock();
		
	</script>
	
</body>

</html>

