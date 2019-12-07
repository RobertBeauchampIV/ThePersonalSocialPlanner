<?php
require_once("database.php");
global $db;

$username = $_POST['username'];


$table = $db->query("SELECT * FROM user WHERE username = \"$username\"");

if($table->rowCount() == 0)
{
    echo"Username does not exist!";
}
else
{        
    $result = $table->fetchAll();
    $userId = $result[0]->user_id;//get USER's id

    //CourseId:blah|CourseName:blah|CourseInstructor:blah|MeetDays:blah|red:blah|blue:blah|green:blah;
    $table2 = $db->query("SELECT course.course_id,course_name,course_instructor,meeting_days,red,blue,green FROM course,enrolled_in,user WHERE course.course_id = enrolled_in.course_id AND user.user_id = enrolled_in.user_id AND user.user_id = $userId ORDER BY course_name ASC");
    if($table2->rowCount() == 0)
    {
        echo"You are not enrolled in any courses";
    }
    else
    {
        $result2 = $table2->fetchAll();
        foreach($result2 as $x)
        {
            echo"CourseId:$x->course_id|CourseName:$x->course_name|CourseInstructor:$x->course_instructor|MeetDays:$x->meeting_days|Red:$x->red|Blue:$x->blue|Green:$x->green;";
        }
    }

}

?>
