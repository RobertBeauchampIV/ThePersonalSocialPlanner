<?php
require_once("database.php");
global $db;


$username = $_POST['username'];
$password = $_POST['password'];


if($username == "" && $password == "")
{
echo"All fields are blank, Nothing was done";
}
else
{
$table = $db->query("Select * from user where username =\"$username\"");
if($table->rowCount() > 0)
{
    $result = $table->fetchAll();

    if($password == $result[0]->user_password)
    {
        if(1 == $result[0]->user_type)
        {
            echo "Admin Login Success";
        }
        else
            echo "Login Success";
    }
    else
    {
        echo"Login Failed: Wrong Password";
    }
}
else
{
    echo"Username was not found";
}
}
?>
