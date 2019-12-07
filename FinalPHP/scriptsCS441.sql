CREATE TABLE university(
    university_id int AUTO_INCREMENT,
    university_name varchar(100),
    CONSTRAINT university_pk PRIMARY KEY(university_id)
);

CREATE TABLE course(
    course_id int AUTO_INCREMENT,
    course_name varchar(100),
    course_instructor varchar(100),
    meeting_days varchar(150),
    university_id int,
    CONSTRAINT course_pk PRIMARY KEY(course_id),
    CONSTRAINT course_fk FOREIGN KEY(university_id) REFERENCES university(university_id)
);

CREATE TABLE user(
    user_id int AUTO_INCREMENT,
    username varchar(20),
    user_password varchar(75),
    first_name varchar(100),
    last_name varchar(100),
    current_year varchar(100),
    security_que varchar(100),
    security_ans varchar(100),       
    university_id int,
    user_type tinyint(1) DEFAULT 0,
    CONSTRAINT user_pk PRIMARY KEY(user_id),
    CONSTRAINT user_fk FOREIGN KEY(university_id) REFERENCES university(university_id)
);

CREATE TABLE enrolled_in(
    course_id int,
    user_id int,
    color varchar(50),
    CONSTRAINT member_of_pk PRIMARY KEY(course_id,user_id),
    CONSTRAINT member_of_fk1 FOREIGN KEY(course_id) REFERENCES course(course_id),
    CONSTRAINT member_of_fk2 FOREIGN KEY(user_id) REFERENCES user(user_id)
);

CREATE TABLE task(
    task_id int AUTO_INCREMENT,
    title varchar(150),
    details varchar(300),
    priority int DEFAULT 0,
    occurrence_date date,
    course_id int,
    CONSTRAINT task_pk PRIMARY KEY(task_id),
    CONSTRAINT task_fk FOREIGN KEY(course_id) REFERENCES course(course_id)
);

CREATE TABLE make(
    task_id int,
    user_id int,
    CONSTRAINT make_pk PRIMARY KEY(task_id,user_id),
    CONSTRAINT make_fk1 FOREIGN KEY(task_id) REFERENCES task(task_id),
    CONSTRAINT make_fk2 FOREIGN KEY(user_id) REFERENCES user(user_id)
);