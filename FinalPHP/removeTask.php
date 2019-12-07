<?php
require_once("database.php");
global $db;
//UPDATED BY JERRY ON 11/30/2019
//THIS WILL REMOVE A TASK FOR A SPECIFIC PERSONS ACCOUNT
$task_id = $_POST['task_id'];
$username = $_POST['username'];

$table = $db->query("Select * from user where username =\"$username\"");
$result = $table->fetchAll();
$user_id = $result[0]->user_id;


$db->query("DELETE FROM make WHERE task_id = $task_id AND user_id = $user_id");
echo"Task Successfully Removed";

?>
