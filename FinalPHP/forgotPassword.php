<?php
require_once("database.php");
global $db;


$username = $_POST['username'];

$table = $db->query("Select * from user where username =\"$username\"");//FIND USERNAME

if($table->rowCount() == 0)//IF NO USERNAME IS RETURNED
{
    echo"Username Does Not Exist";
}
else//THE USER EXISTS
{
    $result = $table->fetchAll();
    $recovery_question = $result[0]->security_que;
    echo $recovery_question; //output the recovery question
}

?>
