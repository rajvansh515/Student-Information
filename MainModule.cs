using System;
using StudentInformationSystem.Repository;
using StudentInformationSystem.Model;
using StudentInformationSystem.Exceptions;

namespace SIS
{
    internal class MainModule
    {
        private ServiceImplementation service;

        public MainModule(
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            ITeacherRepository teacherRepository,
            IEnrollmentRepository enrollmentRepository,
            IPaymentRepository paymentRepository)
        {
            service = new ServiceImplementation(
                studentRepository,
                courseRepository,
                teacherRepository,
                enrollmentRepository,
                paymentRepository);
        }

        public void ShowMenu()
        {
            bool continueRunning = true;

            while (continueRunning)
            {
                Console.WriteLine("\nStudent Information System");
                Console.WriteLine("Select option:");
                Console.WriteLine("1. Enroll Student in a Course");
                Console.WriteLine("2. Assign Teacher to Course");
                Console.WriteLine("3. Record Payment");
                Console.WriteLine("4. Enrollment Report");
                Console.WriteLine("5. Payment Report");
                Console.WriteLine("6. Course Statistics");
                Console.WriteLine("7. Add New Student");
                Console.WriteLine("8. Exit");
                Console.Write("Choice: ");

                var input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            EnrollStudent();
                            break;
                        case "2":
                            AssignTeacherToCourse();
                            break;
                        case "3":
                            RecordPayment();
                            break;
                        case "4":
                            GenerateEnrollmentReport();
                            break;
                        case "5":
                            GeneratePaymentReport();
                            break;
                        case "6":
                            CalculateCourseStatistics();
                            break;
                        case "7":
                            AddNewStudent();
                            break;
                        case "8":
                            continueRunning = false;
                            Console.WriteLine("Exiting system...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public void AddNewStudent()
        {
            Console.WriteLine("\nAdd Student");
            int studentId = service.AddStudentFromInput();
            Console.WriteLine($"New student added successfully. Student ID: {studentId}");

            Console.WriteLine("Do you want to enroll this student in courses? (Y/N)");
            if (Console.ReadLine().Trim().ToUpper() == "Y")
            {
                EnrollStudentInMultipleCourses(studentId);
            }
        }

        private void EnrollStudentInMultipleCourses(int studentId)
        {
            while (true)
            {
                Console.Write("Enter Course ID to enroll / 0-finish");
                int courseId = int.Parse(Console.ReadLine());
                if (courseId == 0) break;

                service.EnrollStudentInCourse(studentId, courseId);
                Console.WriteLine("Student enrolled successfully.");
            }
        }

        private void EnrollStudent()
        {
            Console.WriteLine("\nEnroll a Student in a Course");

            int studentId;
            while (true)
            {
                Console.Write("Student ID: ");
                if (int.TryParse(Console.ReadLine(), out studentId))
                    break;
                Console.WriteLine("Invalid input.");
            }

            int courseId;
            while (true)
            {
                Console.Write("Enter Course ID: ");
                if (int.TryParse(Console.ReadLine(), out courseId))
                    break;
                Console.WriteLine("Invalid input.");
            }

            try
            {
                service.EnrollStudentInCourse(studentId, courseId);
                Console.WriteLine("Student enrolled successfully");
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DuplicateEnrollmentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occurred: " + ex.Message);
            }
        }

        public void AssignTeacherToCourse()
        {
            Console.WriteLine("\nAssign Teacher to a Course");

            // Fetch and print all teachers with their assigned courses
            var teachersWithCourses = service.GetTeachersWithCourses();
            Console.WriteLine("Teachers and Assigned Courses:\n");

            foreach (var (teacher, courses) in teachersWithCourses)
            {
                Console.WriteLine($"Teacher: {teacher.FirstName} {teacher.LastName} (ID: {teacher.TeacherId})");
                if (courses.Any())
                {
                    Console.WriteLine("Assigned Courses:");
                    foreach (var course in courses)
                    {
                        Console.WriteLine($" - ID: {course.CourseId}, Title: {course.CourseName}");
                    }
                }
                else
                {
                    Console.WriteLine(" No assigned courses.");
                }
                Console.WriteLine(new string('-', 5)); // Visual separator
            }

            // Fetch and print all available courses
            var allCourses = service.GetAllCourses();
            Console.WriteLine("Available Courses:");
            foreach (var course in allCourses)
            {
                Console.WriteLine($"ID: {course.CourseId}, Title: {course.CourseName}");
            }

            // Prompt user for input
            Console.Write("Teacher Id: ");
            int teacherId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Course Id: ");
            int courseId = Convert.ToInt32(Console.ReadLine());

            // Assign the teacher to the course
            service.AssignTeacherToCourse(teacherId, courseId);
            Console.WriteLine("Teacher assigned.");

            // Fetch and print updated teachers with their assigned courses again
            teachersWithCourses = service.GetTeachersWithCourses(); // Fetch updated data
            Console.WriteLine("\nUpdated Teachers and Assigned Courses:\n");

            foreach (var (teacher, courses) in teachersWithCourses)
            {
                Console.WriteLine($"Teacher: {teacher.FirstName} {teacher.LastName} (ID: {teacher.TeacherId})");
                if (courses.Any())
                {
                    Console.WriteLine("Assigned Courses:");
                    foreach (var course in courses)
                    {
                        Console.WriteLine($" - ID: {course.CourseId}, Title: {course.CourseName}");
                    }
                }
                else
                {
                    Console.WriteLine("No assigned courses");
                }
                Console.WriteLine(new string('-', 5)); // Visual separator
            }
        }

        public void RecordPayment()
        {
            Console.WriteLine("\nRecord Payment"); 

            // Now prompt for new payment details
            Console.Write("Student ID: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Payment Amount: ");
            decimal paymentAmount = decimal.Parse(Console.ReadLine());
            Console.Write("Payment Date (YYYY-MM-DD): ");
            DateTime paymentDate = DateTime.Parse(Console.ReadLine());

            service.RecordPayment(studentId, paymentAmount, paymentDate);
            Console.WriteLine("Payment recorded.");
            // Fetch all payments for display
            var allPayments = service.GetAllPayments(); // Assuming you have a method to get all payments
            Console.WriteLine("Payments Index");

            // Print all payments in a formatted manner
            foreach (var payment in allPayments)
            {
                Console.WriteLine($"Payment ID: {payment.PaymentId}, Student: {payment.Student.FirstName} {payment.Student.LastName}, Amount: {payment.Amount:C}, Date: {payment.PaymentDate:yyyy-MM-dd}");
            }
        }

        public void GenerateEnrollmentReport()
        {
            Console.WriteLine("\nEnrollment Report");
            Console.Write("Enter Course id: ");
            int cid = Convert.ToInt32(Console.ReadLine());
            service.GenerateEnrollmentReport(cid);
        }

        public void GeneratePaymentReport()
        {
            Console.WriteLine("\nPayment Report");
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            service.GeneratePaymentReport(studentId);
        }

        public void CalculateCourseStatistics()
        {
            Console.WriteLine("\nCourse Statistics");
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            service.CalculateCourseStatistics(courseId);
        }
    }
}