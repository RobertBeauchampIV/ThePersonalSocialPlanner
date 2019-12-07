<?php
require_once("database.php");
global $db;

$course_id = $_POST['course_id'];

$table = $db->query("SELECT course.course_name,user.username,user.first_name,user.last_name  
    FROM course,user,enrolled_in 
    WHERE enrolled_in.course_id =course.course_id 
    AND user.user_id = enrolled_in.user_id 
    AND course.course_id = $course_id 
    ORDER BY user.last_name ASC");


if($table->rowCount() == 0)
{
    echo"No users enrolled in this course";
}
else
{
    $result = $table->fetchAll();
    foreach($result as $x)
    {
        echo"CourseName:$x->course_name|Username:$x->username|FirstName:$x->first_name|LastName:$x->last_name;";//OUTPUTS THE COURSE NAME AND USERNAME for each user enrolled in the course
    }
}
?>
