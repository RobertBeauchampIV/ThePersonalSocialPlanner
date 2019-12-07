<?php
require_once("database.php");
global $db;

$username = $_POST['username'];
$courseId = $_POST['courseId'];



$table = $db->query("SELECT * FROM user WHERE username = \"$username\"");

if($table->rowCount() == 0)
{
    echo"Username does not exist!";
}
else
{        
    $result = $table->fetchAll();
    $userId = $result[0]->user_id;//get USER's id

    //"ENROLL"
    $db->query("DELETE FROM enrolled_in WHERE course_id = $courseId AND user_id = $userId");
    echo"Course Successfully Dropped";
}
?>
