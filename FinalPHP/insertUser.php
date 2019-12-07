<?php
require_once("database.php");
global $db;

/*
$username = "PikachuSenpai";
$password = "Naruto94";
$firstname = "Robert";
$lastname = "Beauchamp";
$year = "Senior";
$securityQuestion = "First Pets Name?";
$securityQuestionAnswer = "Panda";
$universityId = 1;
//0 for normal users 1 for admins
$userType = 1;
*/


$username = $_POST['username'];
$password = $_POST['password'];
$firstname = $_POST['firstname'];
$lastname = $_POST['lastname'];
$year = $_POST['year'];
$securityQuestion = $_POST['securityQuestion'];
$securityQuestionAnswer = $_POST['securityQuestionAnswer'];
$universityId = 1;
//0 for normal users 1 for admins
$userType = 0;


if($username == "" && $password == "" && $firstname == "" && $lastname == "" && $year == "" && $securityQuestion == "" && $securityQuestionAnswer == "")
{
echo"All fields are blank, Nothing was done";
}
else
{
$table = $db->query("Select * from user where username =\"$username\"");
if($table->rowCount() == 0)
{
    $sql = "INSERT INTO user(username, user_password, first_name, last_name, current_year, security_que, security_ans, university_id, user_type) VALUES(\"$username\",\"$password\",\"$firstname\",\"$lastname\",\"$year\",\"$securityQuestion\",\"$securityQuestionAnswer\",\"$universityId\",\"$userType\")";
    $result = $db->query($sql);
    echo"User Creation Successful";
}
else
{
    echo"Username Already Exists";
}
}
?>
