<?php
require_once("database.php");
global $db;

//UPDATED BY JERRY ON 11/30/2019
$username = $_POST['username'];
$date = $_POST['date'];

$table = $db->query("Select * from user where username =\"$username\"");
$result = $table->fetchAll();
$user_id = $result[0]->user_id;//GET USER ID


//UPDATED VERSION!!!!
//------Returns all the TASKS for a specific DATE for a specific USER --SideNote: WOW THIS QUERY TOOK SOME TIME TO WRITE!! 
$table2=$db->query("SELECT DISTINCT task.task_id,title,details,priority,occurrence_date,task.course_id,course_name 
FROM task,course,make 
WHERE occurrence_date = \"$date\"
AND make.task_id = task.task_id
AND task.course_id = course.course_id
AND make.user_id = $user_id");

if($table2->rowCount() > 0)//THERE ARE RESULTS
{
    $result2 = $table2->fetchAll();

    //output all the info
    foreach($result2 as $x)
    {
        echo"TaskId:$x->task_id|Title:$x->title|Details:$x->details|Priority:$x->priority|OccurrenceDate:$x->occurrence_date|CourseName:$x->course_name|CourseId:$x->course_id;";
    }
}
else{
    echo"No tasks on this date";
}


?>
