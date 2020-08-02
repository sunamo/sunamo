using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

public class GoogleContactExportRow : INotifyPropertyChanged
{
    string name = "";
    public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

    string givenName = "";
    public string GivenName { get { return givenName; } set { givenName = value; OnPropertyChanged("GivenName"); } }

    string additionalName = "";
    public string AdditionalName { get { return additionalName; } set { additionalName = value; OnPropertyChanged("AdditionalName"); } }

    string familyName = "";
    public string FamilyName { get { return familyName; } set { familyName = value; OnPropertyChanged("FamilyName"); } }

    string yomiName = "";
    public string YomiName { get { return yomiName; } set { yomiName = value; OnPropertyChanged("YomiName"); } }

    string givenNameYomi = "";
    public string GivenNameYomi { get { return givenNameYomi; } set { givenNameYomi = value; OnPropertyChanged("GivenNameYomi"); } }

    string additionalNameYomi = "";
    public string AdditionalNameYomi { get { return additionalNameYomi; } set { additionalNameYomi = value; OnPropertyChanged("AdditionalNameYomi"); } }

    string familyNameYomi = "";
    public string FamilyNameYomi { get { return familyNameYomi; } set { familyNameYomi = value; OnPropertyChanged("FamilyNameYomi"); } }

    string namePrefix = "";
    public string NamePrefix { get { return namePrefix; } set { namePrefix = value; OnPropertyChanged("NamePrefix"); } }

    string nameSuffix = "";
    public string NameSuffix { get { return nameSuffix; } set { nameSuffix = value; OnPropertyChanged("NameSuffix"); } }

    string initials = "";
    public string Initials { get { return initials; } set { initials = value; OnPropertyChanged("Initials"); } }

    string nickname = "";
    public string Nickname { get { return nickname; } set { nickname = value; OnPropertyChanged("Nickname"); } }

    string shortName = "";
    public string ShortName { get { return shortName; } set { shortName = value; OnPropertyChanged("ShortName"); } }

    string maidenName = "";
    public string MaidenName { get { return maidenName; } set { maidenName = value; OnPropertyChanged("MaidenName"); } }

    string birthday = "";
    public string Birthday { get { return birthday; } set { birthday = value; OnPropertyChanged("Birthday"); } }

    string gender = "";
    public string Gender { get { return gender; } set { gender = value; OnPropertyChanged("Gender"); } }

    string location = "";
    public string Location { get { return location; } set { location = value; OnPropertyChanged("Location"); } }

    string billingInformation = "";
    public string BillingInformation { get { return billingInformation; } set { billingInformation = value; OnPropertyChanged("BillingInformation"); } }

    string directoryServer = "";
    public string DirectoryServer { get { return directoryServer; } set { directoryServer = value; OnPropertyChanged("DirectoryServer"); } }

    string mileage = "";
    public string Mileage { get { return mileage; } set { mileage = value; OnPropertyChanged("Mileage"); } }

    string occupation = "";
    public string Occupation { get { return occupation; } set { occupation = value; OnPropertyChanged("Occupation"); } }

    string hobby = "";
    public string Hobby { get { return hobby; } set { hobby = value; OnPropertyChanged("Hobby"); } }

    string sensitivity = "";
    public string Sensitivity { get { return sensitivity; } set { sensitivity = value; OnPropertyChanged("Sensitivity"); } }

    string priority = "";
    public string Priority { get { return priority; } set { priority = value; OnPropertyChanged("Priority"); } }

    string subject = "";
    public string Subject { get { return subject; } set { subject = value; OnPropertyChanged("Subject"); } }

    string notes = "";
    public string Notes { get { return notes; } set { notes = value; OnPropertyChanged("Notes"); } }

    string language = "";
    public string Language { get { return language; } set { language = value; OnPropertyChanged("Language"); } }

    string photo = "";
    public string Photo { get { return photo; } set { photo = value; OnPropertyChanged("Photo"); } }

    string groupMembership = "";
    public string GroupMembership { get { return groupMembership; } set { groupMembership = value; OnPropertyChanged("GroupMembership"); } }

    string eMail1Type = "";
    public string EMail1Type { get { return eMail1Type; } set { eMail1Type = value; OnPropertyChanged("EMail1Type"); } }

    string eMail1Value = "";
    public string EMail1Value { get { return eMail1Value; } set { eMail1Value = value; OnPropertyChanged("EMail1Value"); } }

    string phone1Type = "";
    public string Phone1Type { get { return phone1Type; } set { phone1Type = value; OnPropertyChanged("Phone1Type"); } }

    string phone1Value = "";
    public string Phone1Value { get { return phone1Value; } set { phone1Value = value; OnPropertyChanged("Phone1Value"); } }

    string address1Type = "";
    public string Address1Type { get { return address1Type; } set { address1Type = value; OnPropertyChanged("Address1Type"); } }

    string address1Formatted = "";
    public string Address1Formatted { get { return address1Formatted; } set { address1Formatted = value; OnPropertyChanged("Address1Formatted"); } }

    string address1Street = "";
    public string Address1Street { get { return address1Street; } set { address1Street = value; OnPropertyChanged("Address1Street"); } }

    string address1City = "";
    public string Address1City { get { return address1City; } set { address1City = value; OnPropertyChanged("Address1City"); } }

    string address1POBox = "";
    public string Address1POBox { get { return address1POBox; } set { address1POBox = value; OnPropertyChanged("Address1POBox"); } }

    string address1Region = "";
    public string Address1Region { get { return address1Region; } set { address1Region = value; OnPropertyChanged("Address1Region"); } }

    string address1PostalCode = "";
    public string Address1PostalCode { get { return address1PostalCode; } set { address1PostalCode = value; OnPropertyChanged("Address1PostalCode"); } }

    string address1Country = "";
    public string Address1Country { get { return address1Country; } set { address1Country = value; OnPropertyChanged("Address1Country"); } }

    string address1ExtendedAddress = "";
    public string Address1ExtendedAddress { get { return address1ExtendedAddress; } set { address1ExtendedAddress = value; OnPropertyChanged("Address1ExtendedAddress"); } }

    string organization1Type = "";
    public string Organization1Type { get { return organization1Type; } set { organization1Type = value; OnPropertyChanged("Organization1Type"); } }

    string organization1Name = "";
    public string Organization1Name { get { return organization1Name; } set { organization1Name = value; OnPropertyChanged("Organization1Name"); } }

    string organization1YomiName = "";
    public string Organization1YomiName { get { return organization1YomiName; } set { organization1YomiName = value; OnPropertyChanged("Organization1YomiName"); } }

    string organization1Title = "";
    public string Organization1Title { get { return organization1Title; } set { organization1Title = value; OnPropertyChanged("Organization1Title"); } }

    string organization1Department = "";
    public string Organization1Department { get { return organization1Department; } set { organization1Department = value; OnPropertyChanged("Organization1Department"); } }

    string organization1Symbol = "";
    public string Organization1Symbol { get { return organization1Symbol; } set { organization1Symbol = value; OnPropertyChanged("Organization1Symbol"); } }

    string organization1Location = "";
    public string Organization1Location { get { return organization1Location; } set { organization1Location = value; OnPropertyChanged("Organization1Location"); } }

    string organization1JobDescription = "";
    public string Organization1JobDescription { get { return organization1JobDescription; } set { organization1JobDescription = value; OnPropertyChanged("Organization1JobDescription"); } }

    string website1Type = "";
    public string Website1Type { get { return website1Type; } set { website1Type = value; OnPropertyChanged("Website1Type"); } }

    string website1Value = "";
    public string Website1Value { get { return website1Value; } set { website1Value = value; OnPropertyChanged("Website1Value"); } }

    string customField1Type = "";
    public string CustomField1Type { get { return customField1Type; } set { customField1Type = value; OnPropertyChanged("CustomField1Type"); } }

    string customField1Value = "";
    public string CustomField1Value { get { return customField1Value; } set { customField1Value = value; OnPropertyChanged("CustomField1Value"); } }

    string customField2Type = "";
    public string CustomField2Type { get { return customField2Type; } set { customField2Type = value; OnPropertyChanged("CustomField2Type"); } }

    string customField2Value = "";

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged(string propName)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(propName));
    }

    public string CustomField2Value { get { return customField2Value; } set { customField2Value = value; OnPropertyChanged("CustomField2Value"); } }


}