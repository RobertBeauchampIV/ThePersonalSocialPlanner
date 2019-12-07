<?php
require_once("database.php");
global $db;



$table = $db->query("Select * from course ORDER BY course_name ASC");

if($table->rowCount() == 0)
{
    echo"No Courses Available";
}
else
{

    $result = $table->fetchAll();
    foreach ($result as $x)
    {
        echo"CourseId:$x->course_id|CourseName:$x->course_name|CourseInstructor:$x->course_instructor|MeetDays:$x->meeting_days;";
    }
}

?>
