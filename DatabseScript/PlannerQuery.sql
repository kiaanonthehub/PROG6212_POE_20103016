CREATE DATABASE StudyPlanner;
USE StudyPlanner;

CREATE TABLE tblStudent
(
student_number INT NOT NULL PRIMARY KEY,
student_name VARCHAR(25),
student_surname VARCHAR(25),
student_email VARCHAR(50) NOT NULL,
student_hash_password VARCHAR(100) NOT NULL,
start_date DATE NOT NULL, 
number_of_weeks INT NOT NULL
);

CREATE TABLE tblSemester
(
semester_id INT NOT NULL IDENTITY PRIMARY KEY,
start_date DATE NOT NULL, 
number_of_weeks INT,
);

CREATE TABLE tblModule
(
module_id VARCHAR(8) NOT NULL PRIMARY KEY,
module_name VARCHAR(25) NOT NULL,
module_credits INT NOT NULL,
module_class_hours INT NOT NULL,
);

CREATE TABLE tblStudentModule
(
student_module_id INT NOT NULL IDENTITY PRIMARY KEY, 
student_number INT,
module_id VARCHAR(8),
module_self_study_hour DECIMAL(5,2),
study_hours_remains DECIMAL(5,2),
study_reminder_day VARCHAR(25),
FOREIGN KEY (student_number) REFERENCES tblStudent (student_number),
FOREIGN KEY (module_id) REFERENCES tblModule (module_id)
);

CREATE TABLE tblTrackStudies
(
track_studies_id INT NOT NULL IDENTITY PRIMARY KEY,
hours_worked DECIMAL(5,2),
date_worked DATE,
week_number INT ,
semester_id INT,
student_number INT,
module_id VARCHAR(8),
FOREIGN KEY (module_id) REFERENCES tblModule (module_id),
FOREIGN KEY (student_number) REFERENCES tblStudent (student_number)
);

