<?php
require_once("database.php");
global $db;
//UPDATED BY JERRY ON 11/30/2019 AND AGAIN ON 12/1
//added the checking to make sure no flooding of the same task

$task_id = $_POST['task_id']; //Send this task...
$username = $_POST['username']; //To this user!

$table = $db->query("Select * from user where username =\"$username\"");
$result = $table->fetchAll();
$user_id = $result[0]->user_id;//GET USERID


//query to check if TASK already shared/exists for that user
$checkTable = $db->query("SELECT * FROM make WHERE task_id = $task_id AND user_id = $user_id");


// if > 0 , then that means it already exists
if($checkTable->rowCount() > 0) 
{
    echo"Task has already been shared!";
}
else//does not exist -- SO SHARE
{
    $db->query("INSERT INTO make(task_id,user_id) VALUES($task_id,$user_id)");
    echo"Task has been successfully shared";
}



?>
