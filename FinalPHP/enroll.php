<?php
require_once("database.php");
global $db;

$username = $_POST['username'];
$courseId = $_POST['courseId'];
$red = $_POST['red'];
$blue = $_POST['blue'];
$green = $_POST['green'];


$table = $db->query("SELECT * FROM user WHERE username = \"$username\"");

if($table->rowCount() == 0)
{
    echo"Username does not exist!";
}
else
{        
    $result = $table->fetchAll();
    $userId = $result[0]->user_id;//get USER's id

    //this query will return a row if the user is already enrolled in course
    $table2 = $db->query("SELECT * FROM enrolled_in WHERE course_id =\"$courseId\" AND user_id=\"$userId\"");
    if($table2->rowCount() > 0)
	{
    	echo"You're already enrolled in this course!";
	}
	else
	{
    	//"ENROLL"
	    $db->query("INSERT INTO enrolled_in(course_id,user_id,red,blue,green) VALUES ($courseId,$userId,$red,$blue,$green)");
    	echo"User Successfully Enrolled";
	}
}

?>
