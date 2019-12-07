<?php
require_once("database.php");
global $db;
//UPDATED BY JERRY ON 11/30/2019

//------GET ALL THE POST VARIABLES REQUIRED TO ADD TASK
$username = $_POST['username']; //
$course_id = $_POST['course_id'];//
$title = $_POST['title']; //needed
$details = $_POST['details'];//needed
$priority = $_POST['priority'];//needed
$occurrence_date = $_POST['occurrence_date'];//needed <NOTE:string>
//--------------------------------------------------------------

//GET USER ID
$table = $db->query("Select * from user where username =\"$username\"");
$result = $table->fetchAll();
$user_id = $result[0]->user_id;



//CREATE THE TASK
$db->query("INSERT INTO task(title,details,priority,occurrence_date,course_id) VALUES(\"$title\",\"$details\",$priority,\"$occurrence_date\",$course_id)");


//GET TASK ID
$table2 = $db->query("SELECT task_id FROM task ORDER BY task_id DESC LIMIT 0, 1");
$result2 = $table2->fetchAll();
$new_task_id = $result2[0]->task_id;



//ADD IT FOR THAT USER (by adding to the MAKE table)
$db->query("INSERT INTO make(task_id,user_id) VALUES($new_task_id,$user_id)");


//SUCCESS -- DONE
echo"Successfully Added"; //IF this outputs then The above query worked...



?>
