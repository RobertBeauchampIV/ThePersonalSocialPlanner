<?php
require_once("database.php");
global $db;

//GET VARIABLES
$username = $_POST['username'];
$updated_password = $_POST['updated_password'];

$table = $db->query("SELECT * FROM user WHERE username =\"$username\"");

if($table->rowCount() > 0)//CHECK IF USER ACTUALLY EXISTS
{
    $db->query("UPDATE user SET user_password=\"$updated_password\" WHERE username =\"$username\"");
    echo "Update Successful!";
}
else
{
    echo"User does not exist";
}

?>
