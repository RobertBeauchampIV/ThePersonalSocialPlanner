<?php
require_once("database.php");
global $db;

/*
$courseName = "CS 441 - Software Engineering";
$courseInstructor = "Yongjie Zheng";
$meetingDays = "Tuesday/Thursday: 1:00PM - 2:15PM";
$universityId = 1;
*/


$courseName = $_POST['courseName'];
$courseInstructor = $_POST['instructor'];
$meetingDays = $_POST['meetingDays'];
$universityId = 1;


if($courseName == "" && $courseInstructor == "" && $meetingDays == "")
{
echo"All fields are blank, Nothing was done";
}
else
{
$table = $db->query("Select * from course where course_name =\"$courseName\"");
if($table->rowCount() == 0)
{
    $sql = "INSERT INTO course(course_name, course_instructor, meeting_days, university_id) VALUES(\"$courseName\",\"$courseInstructor\",\"$meetingDays\",\"$universityId\")";
    $result = $db->query($sql);
    echo"Course Creation Successful";
}
else
{
    echo"Course Already Exists";
}
}

?>
