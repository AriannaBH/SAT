using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA.EF
{
    [MetadataType(typeof(CourseMetadata))]
    public partial class Course
    {

    }

    public class CourseMetadata
    {
        [StringLength(50, ErrorMessage = "50 Character Max!")]
        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "This field is required!")]
        public string CourseName { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Course Description")]
        [Required(ErrorMessage = "This field is required!")]
        public string CourseDescription { get; set; }

        [Display(Name = "Credit Hours")]
        [Required(ErrorMessage = "This field is required!")]
        public byte CreditHours { get; set; }

        [StringLength(250, ErrorMessage = "250 Character Max!")]
        [UIHint("MultilineText")]
        public string Curriculum { get; set; }

        [StringLength(500, ErrorMessage = "500 Character Max!")]
        [UIHint("MultilineText")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment
    {

    }

    public class EnrollmentMetadata
    {
        [Required(ErrorMessage = "This field is required!")]//TODO: This is looking for an existing student ID, not sure if other meta data exists for it yet
        public int StudentID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public int ScheduledClassId { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[-Date Not Provided-]")]
        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Enrollment Date")]
        public System.DateTime EnrollmentDate { get; set; }
    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass
    {
        public string ScheduledCourse
        {
            get { return $"{StartDate:d} - {Course.CourseName} - {Location}"; }
        }
    }

    public class ScheduledClassMetadata
    {
        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[-Date Not Provided-]")]
        [Display(Name = "Start Date")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[-Date Not Provided-]")]
        [Display(Name = "End Date")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [StringLength(40, ErrorMessage = "40 Character Max!")]
        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [StringLength(20, ErrorMessage = "20 Character Max!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Schedule Class Status ID")]
        public int SCSID { get; set; }
    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {

    }

    public class StudentMetadata
    {
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(20, ErrorMessage = "20 Character Max!")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [StringLength(20, ErrorMessage = "20 Character Max!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [StringLength(15, ErrorMessage = "15 Character Max!")]
        public string Major { get; set; }
        
        [StringLength(50, ErrorMessage = "50 Character Max!")]
        public string Address { get; set; }
        
        [StringLength(25, ErrorMessage = "25 Character Max!")]
        public string City { get; set; }
        
        [StringLength(2, ErrorMessage = "2 Character Max!")]
        public string State { get; set; }
        
        [StringLength(10, ErrorMessage = "10 Character Max!")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        
        [StringLength(13, ErrorMessage = "13 Character Max!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [StringLength(60, ErrorMessage = "60 Character Max!")]
        public string Email { get; set; }
        
        [StringLength(100, ErrorMessage = "100 Character Max!")]
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Student Status ID")]
        public int SSID { get; set; }
        
        
    }

    [MetadataType(typeof(ScheduledClassStatusMetadata))]
    public partial class ScheduledClassStatus
    {

    }

    public class ScheduledClassStatusMetadata
    {
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(50, ErrorMessage = "50 Character Max!")]
        [Display(Name = "Class Status")]
        public string SCSName { get; set; }
    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus
    {

    }
    
    public class StudentStatusMetadata
    {
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(30, ErrorMessage = "30 Character Max!")]
        [Display(Name ="Student Status")]
        public string SSName { get; set; }
        
        [StringLength(250, ErrorMessage = "250 Character Max!")]
        public string SSDescription { get; set; }
    }
}
