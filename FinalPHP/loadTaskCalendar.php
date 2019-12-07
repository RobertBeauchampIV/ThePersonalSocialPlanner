<?php
require_once("database.php");
global $db;
//UPDATED BY JERRY ON 11/30/2019
$username = $_POST['username'];
$date = $_POST['date'];

//GET USER ID
$table = $db->query("Select * from user where username =\"$username\"");
$result = $table->fetchAll();
$user_id = $result[0]->user_id;


//Returns all the TASKS for a specific DATE for a specific USER
$table2=$db->query("SELECT DISTINCT task.task_id,title,details,priority,occurrence_date,task.course_id,course_name 
FROM task,course,make 
WHERE occurrence_date = \"$date\"
AND make.task_id = task.task_id
AND task.course_id = course.course_id
AND make.user_id = $user_id");


//OUTPUT NUMBER
echo $table2->rowCount(); //(e.g.) if 5 tasks are found  -- this outputs: "5"


?>
