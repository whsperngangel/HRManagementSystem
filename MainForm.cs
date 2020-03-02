using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace HRManagementSystem
{
    public partial class MainForm : Form
    {
        #region Variables
        private string _employeeID = "",
                       _firstName = "",
                       _middleName = "",
                       _lastName = "",
                       _age = "",
                       _gender = "",
                       _maritalStatus = "",
                       _nationality = "",
                       _currentAddress = "",
                       _permanentAddress = "",
            
                       _email = "",
            
                       _tin = "",
                       _sss = "",
                       _pagibig = "",
                       _philHealth = "",
            
                       _position = "",
                       _department = "",
                       _branch = "",
                       _head = "",
                       _los= "",
            
                       _graduate = "",
                       _college = "",
                       _seniorHigh = "",
                       _juniorHigh = "",
                       _elementary = "";

        private int _globe = 0,
                    _smart = 0,
                    _totalDays = 0,
                    _daysOff = 0,
                    _leave = 0,
                    _minsOfTadiness = 0,
                    _numberOfHalfDays = 0,
                    _numberOfAttendance = 0;


        private DateTime _dob = DateTime.Now,
                         _startDate = DateTime.Now;

        private byte _picture;

        private BasicInfo _basicInfo = new BasicInfo();
        private ContactInfo _contactInfo = new ContactInfo();
        private GovernmentID _governmentID = new GovernmentID();
        private CPosition _cPosition = new CPosition();
        private Education _education = new Education();

        List<BasicInfo> _basicInfos = new List<BasicInfo>();
        List<ContactInfo> _contactInfos = new List<ContactInfo>();
        List<GovernmentID> _governmentIDs = new List<GovernmentID>();
        List<CPosition> _cPositions = new List<CPosition>();
        List<Education> _educations = new List<Education>();

        DBOperation _dbOp = new DBOperation();
        #endregion


        #region Constructors
        public MainForm()
        {
            InitializeComponent();
            ComboBoxLoad();
            ListViewLoad();
        }

        private void ShowData(string employeeID)
        {
            _basicInfo = _basicInfo.RetrieveBasicInfo(employeeID);
            employeeIDTB.Text = _basicInfo.EmployeeID;
            firstNameTB.Text = _basicInfo.FirstName;
            middleNameTB.Text = _basicInfo.MiddleName;
            lastNameTB.Text = _basicInfo.LastName;
            dobDTP.Value = _basicInfo.DOB;
            genderTB.Text = _basicInfo.Gender;
            maritalStatusTB.Text = _basicInfo.MaritalStatus;
            nationalityTB.Text = _basicInfo.Nationality;
            currentAddressTB.Text = _basicInfo.CurrentAddress;
            permanentAddressTB.Text = _basicInfo.PermanentAddress;
            picturePB.Image = Image.FromFile(_basicInfo.Picture.ToString());

            _contactInfo = _contactInfo.RetrieveContactInfo(employeeID);
            employeeIDTB.Text = _contactInfo.EmployeeID;
            globeTB.Text = _contactInfo.Globe;
            smartTB.Text = _contactInfo.Smart;
            emailTB.Text = _contactInfo.Email;

            _governmentID = _governmentID.RetrieveGovernmentID(employeeID);
            employeeIDTB.Text = _governmentID.EmployeeID;
            tinTB.Text = _governmentID.TIN;
            sssTB.Text = _governmentID.SSS;
            pagibigTB.Text = _governmentID.PAGIBIG;
            philhealthTB.Text = _governmentID.PhilHealth;

            _cPosition = _cPosition.RetrieveCPosition(employeeID);
            employeeIDTB.Text = _cPosition.EmployeeID;
            positionTB.Text = detailsPositionTB.Text = _cPosition.Position;
            departmentTB.Text = detailsDepartmentTB.Text = _cPosition.Department;
            branchCB.Text = _cPosition.Branch;
            headTB.Text = _cPosition.Head;
            startDateDTP.Value = _cPosition.StartDate;

            _education = _education.RetrieveEducation(employeeID);
            employeeIDTB.Text = _education.EmployeeID;
            graduateTB.Text = _education.EduAttainment;
            collegeTB.Text = _education.College;
            seniorHighTB.Text = _education.SeniorHigh;
            juniorHighTB.Text = _education.JuniorHigh;
            elementaryTB.Text = _education.Elementary;

            LoadBasicInfo(_employeeID);
            LoadContactInfo(_employeeID);
            LoadGovernmentID(_employeeID);
            LoadCPosition(_employeeID);
            LoadEducation(_employeeID);
        }
        #endregion

        #region Enable, Disable, Clear Fields
        public void EmployeeClear()
        {
            employeeIDTB.Text = "";
            firstNameTB.Text = "";
            middleNameTB.Text = "";
            lastNameTB.Text = "";
            globeTB.Text = "";
            smartTB.Text = "";
            emailTB.Text = "";
            positionTB.Text = "";
            departmentTB.Text = "";

            detailsPositionTB.Text = "";
            detailsDepartmentTB.Text = "";
            detailsBranchTB.Text = "";
            headTB.Text = "";
            startDateDTP.Value = DateTime.Now;
            losTB.Text = "";

            tinTB.Text = "";
            sssTB.Text = "";
            pagibigTB.Text = "";
            philhealthTB.Text = "";

            graduateTB.Text = "";
            collegeTB.Text = "";
            seniorHighTB.Text = "";
            juniorHighTB.Text = "";
            elementaryTB.Text = "";

            dobDTP.Value = DateTime.Now;
            genderTB.Text = "";
            maritalStatusTB.Text = "";
            nationalityTB.Text = "";
            currentAddressTB.Text = "";
            permanentAddressTB.Text = "";

            atotalDaysTB.Text = "";
            daysOffTB.Text = "";
            leaveTB.Text = "";
            totalLateTB.Text = "";
            attendanceTB.Text = "";

            evaluationRTB.Text = "";
            workPerformanceRTB.Text = "";
        }
        public void EmployeeEnable()
        {
            firstNameTB.Enabled = true;
            middleNameTB.Enabled = true;
            lastNameTB.Enabled = true;
            globeTB.Enabled = true;
            smartTB.Enabled = true;
            emailTB.Enabled = true;
            positionTB.Enabled = true;
            departmentTB.Enabled = true;
            picturePB.Enabled = true;

            detailsPositionTB.Enabled = true;
            detailsDepartmentTB.Enabled = true;
            detailsBranchTB.Enabled = true;
            headTB.Enabled = true;
            startDateDTP.Enabled = true;

            tinTB.Enabled = true;
            sssTB.Enabled = true;
            pagibigTB.Enabled = true;
            philhealthTB.Enabled = true;

            graduateTB.Enabled = true;
            collegeTB.Enabled = true;
            seniorHighTB.Enabled = true;
            juniorHighTB.Enabled = true;
            elementaryTB.Enabled = true;

            dobDTP.Enabled = true;
            genderTB.Enabled = true;
            maritalStatusTB.Enabled = true;
            nationalityTB.Enabled = true;
            currentAddressTB.Enabled = true;
            permanentAddressTB.Enabled = true;

            atotalDaysTB.Enabled = true;
            daysOffTB.Enabled = true;
            leaveTB.Enabled = true;
            totalLateTB.Enabled = true;
            attendanceTB.Enabled = true;

            evaluationRTB.Enabled = true;
            workPerformanceRTB.Enabled = true;

            editB.Visible = true;
            cancelB.Visible = true;
        }
        public void EmployeeDisable()
        {
            employeeIDTB.Enabled = false;
            firstNameTB.Enabled = false;
            middleNameTB.Enabled = false;
            lastNameTB.Enabled = false;
            globeTB.Enabled = false;
            smartTB.Enabled = false;
            emailTB.Enabled = false;
            positionTB.Enabled = false;
            departmentTB.Enabled = false;
            picturePB.Enabled = false;

            detailsPositionTB.Enabled = false;
            detailsDepartmentTB.Enabled = false;
            detailsBranchTB.Enabled = false;
            headTB.Enabled = false;
            startDateDTP.Enabled = false;

            tinTB.Enabled = false;
            sssTB.Enabled = false;
            pagibigTB.Enabled = false;
            philhealthTB.Enabled = false;

            graduateTB.Enabled = false;
            collegeTB.Enabled = false;
            seniorHighTB.Enabled = false;
            juniorHighTB.Enabled = false;
            elementaryTB.Enabled = false;

            dobDTP.Enabled = false;
            genderTB.Enabled = false;
            maritalStatusTB.Enabled = false;
            nationalityTB.Enabled = false;
            currentAddressTB.Enabled = false;
            permanentAddressTB.Enabled = false;

            atotalDaysTB.Enabled = false;
            daysOffTB.Enabled = false;
            leaveTB.Enabled = false;
            totalLateTB.Enabled = false;
            attendanceTB.Enabled = false;

            evaluationRTB.Enabled = false;
            workPerformanceRTB.Enabled = false;

            editB.Visible = false;
            cancelB.Visible = false;
        }
        #endregion



        #region Validating

        #endregion



        #region Date Time Picker
        private void dobDTP_ValueChanged(object sender, EventArgs e)
        {
            int _age = DateTime.Today.Year - dobDTP.Value.Year;
            ageTB.Text = _age.ToString();
        }
        private void startDateDTP_ValueChanged(object sender, EventArgs e)
        {
            int losYear = DateTime.Today.Year - startDateDTP.Value.Year;
            int losMonth = DateTime.Today.Month - startDateDTP.Value.Month;
            int losDay = DateTime.Today.Day - startDateDTP.Value.Day;

            losTB.Text = (losYear.ToString() + " Year(s)") + " , " + (losMonth.ToString() + " Month(s)") + " , " + (losDay.ToString() + " Day(s)");
        }
        #endregion


        
        #region Load Data
        public void LoadBasicInfo(string employeeID)
        {
            _basicInfo = _basicInfo.RetrieveBasicInfo(employeeID);
        }
        public void LoadContactInfo(string employeeID)
        {
            _contactInfo = _contactInfo.RetrieveContactInfo(employeeID);
        }
        public void LoadGovernmentID(string employeeID)
        {
            _governmentID = _governmentID.RetrieveGovernmentID(employeeID);
        }
        public void LoadCPosition(string employeeID)
        {
            _cPosition = _cPosition.RetrieveCPosition(employeeID);
        }
        public void LoadEducation(string employeeID)
        {
            _education = _education.RetrieveEducation(employeeID);
        }
        #endregion



        #region Picture
        private void employeePB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                picturePB.Image = new Bitmap(openFileDialog.FileName);
            }
            Show();
        }


        public static byte[] ImageToByteArray(Image image)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        #endregion



        #region Combo Box
        private void branchCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _branch = branchCB.Text;
            ShowData(_employeeID);
        }
        private void ComboBoxLoad()
        {
            _cPositions = _cPosition.RetrieveCPositionList();
            foreach (CPosition cp in _cPositions)
            {
                branchCB.Items.Add(cp.Branch);
            }
        }
        #endregion

        #region List View
        private void employeeLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            _employeeID = employeeLV.SelectedItems[0].Text;
            _firstName = employeeLV.SelectedItems[1].Text;
            _lastName = employeeLV.SelectedItems[2].Text;
            _middleName = employeeLV.SelectedItems[3].Text;

            ShowData(_employeeID);
            ShowData(_firstName);
            ShowData(_lastName);
        }
        private void ListViewLoad()
        {
            _basicInfos = _basicInfo.RetrieveBasicInfoList();
            foreach (BasicInfo bi in _basicInfos)
            {
                employeeLV.Items.Add(bi.EmployeeID);
                employeeLV.Items.Add(bi.FirstName);
                employeeLV.Items.Add(bi.LastName);
                employeeLV.Items.Add(bi.MiddleName);
            }
        }
        #endregion



        #region Tool Strip Menu
        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewEmployeeForm newEmployeeForm = new NewEmployeeForm();
            newEmployeeForm.ShowDialog();
        }
        #endregion



        #region Buttons
        private void editB_Click(object sender, EventArgs e)
        {
            switch (editB.Text)
            {
                case "Edit":
                    editB.Text = "Save";
                    EmployeeEnable();
                    break;

                case "Save":
                    if (_firstName != firstNameTB.Text ||
                    _middleName != middleNameTB.Text ||
                    _lastName != lastNameTB.Text ||
                    _dob != dobDTP.Value ||
                    _gender != genderTB.Text ||
                    _maritalStatus != maritalStatusTB.Text ||
                    _nationality != nationalityTB.Text ||
                    _currentAddress != currentAddressTB.Text ||
                    _permanentAddress != permanentAddressTB.Text ||

            //        _globe != int.Parse(globeTB.Text.Trim()) ||
             //       _smart != int.Parse(smartTB.Text.Trim()) ||
                    _email != emailTB.Text ||

                    _tin != tinTB.Text ||
                    _sss != sssTB.Text ||
                    _pagibig != pagibigTB.Text ||
                    _philHealth != philhealthTB.Text ||

                    _position != positionTB.Text ||
                    _position != detailsPositionTB.Text ||
                    _department != departmentTB.Text ||
                    _department != detailsDepartmentTB.Text ||
                    _branch != detailsBranchTB.Text ||
                    _head != headTB.Text ||
                    _startDate != startDateDTP.Value ||

                    _graduate != graduateTB.Text ||
                    _college != collegeTB.Text ||
                    _seniorHigh != seniorHighTB.Text ||
                    _juniorHigh != juniorHighTB.Text ||
                    _elementary != elementaryTB.Text)
                    {
                        _basicInfo = new BasicInfo(_basicInfo.EmployeeID,
                                                   firstNameTB.Text,
                                                   middleNameTB.Text,
                                                   lastNameTB.Text,
                                                   dobDTP.Value,
                                                   genderTB.Text,
                                                   maritalStatusTB.Text,
                                                   nationalityTB.Text,
                                                   currentAddressTB.Text,
                                                   permanentAddressTB.Text,
                                                   byte.Parse(picturePB.Image.ToString()));

                      /*  _contactInfo = new ContactInfo(_contactInfo.EmployeeID,
                                                 //      int.Parse(globeTB.Text.Trim()),
                                                //      int.Parse(smartTB.Text.Trim()),
                                                       emailTB.Text);*/

                        _governmentID = new GovernmentID(_governmentID.EmployeeID,
                                                         tinTB.Text,
                                                         sssTB.Text,
                                                         pagibigTB.Text,
                                                         philhealthTB.Text);

                        _cPosition = new CPosition(_cPosition.EmployeeID,
                                                   positionTB.Text,
                                                   departmentTB.Text,
                                                   branchCB.Text,
                                                   headTB.Text,
                                                   startDateDTP.Value);

                        _education = new Education(_education.EmployeeID,
                                                   graduateTB.Text,
                                                   collegeTB.Text,
                                                   seniorHighTB.Text,
                                                   juniorHighTB.Text,
                                                   elementaryTB.Text);

                        _basicInfo.UpdateBasicInfo(_basicInfo);
                        _contactInfo.UpdateContactInfo(_contactInfo);
                        _governmentID.UpdateGovernmentID(_governmentID);
                        _cPosition.UpdateCPosition(_cPosition);
                        _education.UpdateEducation(_education);

                    }
                    editB.Text = "Edit";
                    EmployeeDisable();
                    break;
            }

            LoadBasicInfo(_employeeID);
            LoadContactInfo(_employeeID);
            LoadGovernmentID(_employeeID);
            LoadCPosition(_employeeID);
            LoadEducation(_employeeID);
        }
        private void cancelB_Click(object sender, EventArgs e)
        {
            EmployeeDisable();

            LoadBasicInfo(_employeeID);
            LoadContactInfo(_employeeID);
            LoadGovernmentID(_employeeID);
            LoadCPosition(_employeeID);
            LoadEducation(_employeeID);
        }
        private void clearB_Click(object sender, EventArgs e)
        {
            EmployeeClear();
        }
        #endregion
    }
}
