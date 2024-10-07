--***** TASK - 1: Database Design *****

--CREATING DATABASE SCHEMA 

Create DATABASE SISDB;
USE SISDB;

--Creating Tables and populating them

-- #STUDENTS TABLE
CREATE TABLE Students(
    student_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,               
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) ,
    date_of_birth DATE NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone_number VARCHAR(15) NOT NULL UNIQUE
);
select * from Students
-- #TEACHER TABLE
CREATE TABLE Teacher(
    teacher_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,                
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) ,
    email VARCHAR(100) NOT NULL UNIQUE
);
select * from Teacher
-- #PAYMENTS TABLE
CREATE TABLE Payments(
    payment_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,                
    student_id INT NOT NULL,                            
    amount INT NOT NULL,
    payment_date DATE NOT NULL,
    CONSTRAINT FK_Student_Payments FOREIGN KEY (student_id) 
        REFERENCES Students(student_id)
        ON DELETE CASCADE ON UPDATE CASCADE    
);
select * from Payments;
-- #COURSES TABLE
CREATE TABLE Courses(
    course_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,                 
    course_name VARCHAR(50) NOT NULL,
    credits INT NOT NULL,
    teacher_id INT ,                            
    CONSTRAINT FK_Teacher_Courses FOREIGN KEY (teacher_id) 
        REFERENCES Teacher(teacher_id)
        ON DELETE SET NULL ON UPDATE CASCADE  
);
select * from Courses
-- #ENROLLMENTS TABLE
CREATE TABLE Enrollments(
    enrollment_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,             
    student_id INT,                            
    course_id INT,                             
    enrollment_date DATE NOT NULL,
    CONSTRAINT FK_Student_Enrollments FOREIGN KEY (student_id) 
        REFERENCES Students(student_id)
        ON DELETE CASCADE ON UPDATE CASCADE,   
    CONSTRAINT FK_Course_Enrollments FOREIGN KEY (course_id) 
        REFERENCES Courses(course_id)
        ON DELETE CASCADE ON UPDATE CASCADE    
);
select*from Enrollments
--INSERTING 10 SAMPLE VALUES IN EACH TABLE (i.e, populating the empty schema)

--Adding Data To Students Table
INSERT INTO Students ( first_name, last_name, date_of_birth, email, phone_number) VALUES
( 'Rajvansh', 'Singh', '2001-12-21', 'rajvansh@gmail.com', '9348858112'),
( 'jia', 'gupta', '1998-12-22', 'jia@yahoo.com', '9823336789'),
( 'karthik', 'Mishra', '2001-07-17', 'karthik@gmail.com', '9934562221'),
( 'mohammad', 'ghajni', '1999-08-02', 'ghajn@gmail.com', '9845431234'),
( 'Vikash', 'Patil', '2001-01-04', 'vikash@gmail.com', '9123432345'),
( 'surya', 'bisht', '2002-12-17', 'surya@gmail.com', '9348888221'),
( 'pranjal', 'Gaitonde', '1998-10-31', 'pranjal@gmail.com', '9823433445'),
( 'rajveer', 'dhillon', '2002-03-02', 'rajvd@gmail.com', '9876588775'),
( 'Amrit', 'ghosle', '2000-04-21', 'amrit@gmail.com', '8877399789'),
( 'Dinesh', 'kumar', '2000-03-11', 'dinesh@gmail.com', '7777512346');


--Adding Data To Teacher Table
INSERT INTO Teacher (first_name, last_name, email) VALUES
('Rajesh', 'Sharma', 'rajesh.sharma@gmail.com'),
('Anjali', 'Verma', 'anjali.verma@gmail.com'),
('Suresh', 'Rao', 'suresh.rao@gmail.com'),
('Priya', 'Kumari', 'priya.kumari@gmail.com'),
('Ravi', 'Patel', 'ravi.patel@gmail.com'),
('Sunita', 'Iyer', 'sunita.iyer@gmail.com'),
('Amit', 'Singh', 'amit.singh@gmail.com'),
('Neha', 'Mehta', 'neha.mehta@gmail.com'),
('Vikas', 'Gupta', 'vikas.gupta@gmail.com'),
('Preeti', 'Jain', 'preeti.jain@gmail.com');
SELECT student_id FROM Students;

--Adding Data To Payments Table
INSERT INTO Payments (student_id, amount, payment_date) VALUES
( 23, 15000, '2024-01-10'),
( 24, 18000, '2024-02-14'),
( 18, 12000, '2024-03-01'),
( 16, 13000, '2024-04-15'),
( 17, 14000, '2024-05-10'),
( 21, 15000, '2024-06-18'),
( 15, 16000, '2024-07-12'),
( 22, 12000, '2024-08-01'),
( 20, 11000, '2024-09-05'),
( 19, 17000, '2024-10-02');

--Adding Data To Courses Table
INSERT INTO Courses ( course_name, credits, teacher_id) VALUES
('Mathematics', 4, 1),
('Physics', 3, 2),
('Chemistry', 3, 3),
('Biology', 4, 4),
('English Literature', 2, 5),
('Computer Science', 5, 6),
('History', 3, 7),
('Geography', 3, 8),
('Economics', 4, 9),
('Political Science', 2, 10);

--Adding Data To Enrollments Table
INSERT INTO Enrollments ( student_id, course_id, enrollment_date) VALUES
(23, 1, '2024-01-15'),
(24, 2, '2024-01-18'),
(18, 3, '2024-01-20'),
(16, 4, '2024-02-05'),
(17, 5, '2024-02-12'),
(21, 6, '2024-02-15'),
(15, 7, '2024-03-01'),
(22, 8, '2024-03-10'),
(20, 9, '2024-03-18'),
(19, 10, '2024-03-25');

select * from Enrollments