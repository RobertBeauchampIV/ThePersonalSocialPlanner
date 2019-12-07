<?php
require_once("database.php");
global $db;

//GET VARIABLES FROM POST
$username = $_POST['username'];
$security_question_answer = $_POST['security_ans'];


$table = $db->query("SELECT * FROM user WHERE security_ans = \"$security_question_answer\" AND username =\"$username\"");

if($table->rowCount() == 0) //IF NO MATCH IS FOUND
{
    echo"Answer Incorrect";
}
else //THERE IS MATCH
{
    echo"Answer Correct";
}

?>
