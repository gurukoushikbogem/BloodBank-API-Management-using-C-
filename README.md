# **Blood Bank Management REST API**

## **Overview**
The **Blood Bank Management REST API** is a project developed using **C#** and **ASP.NET Core**. It allows managing a blood bank's operations, such as adding donor entries, retrieving donor data, updating statuses, deleting entries, pagination, and searching/filtering data.

The API uses an in-memory list (`List<BloodBankModel>`) to store data and supports **CRUD operations**, **search functionalities**, **pagination**, **sorting**, and **filtering**.

---

## **Features**
- **Create, Read, Update, and Delete (CRUD) operations.**
- **Pagination** for efficient data browsing.
- **Search by blood type, donor name, and status.**
- **Sorting** and **filtering** of entries.

---

## **Tools Used**
- **ASP.NET Core**: Framework for developing the API.
- **Swagger**: API documentation and testing interface.
- **Postman**: Manual testing of API endpoints.
- **GitHub**: Repository for code and documentation management.

---

## **API Endpoints**

### **1. CRUD Operations**

#### **Create a New Entry**
- **Endpoint**: `POST /api/bloodbank`
- **Description**: This endpoint allows adding a new donor to the system. The input data is validated to ensure the integrity and accuracy of the donor's information.
- **Validation**:
  - All fields must be present.
  - Mobile number should have 10 digits.
  - Minimum blood donation is 200 mL.
  - Status must be one of: `Available`, `Requested`, `Expired`.

#### **Retrieve All Entries**
- **Endpoint**: `GET /api/bloodbank`
- **Description**: Retrieves all donors available.
- **Validation**: At least one field entry must be present.

#### **Retrieve a Specific Blood Entry by ID**
- **Endpoint**: `GET /api/bloodbank/{id}`

#### **Update a Specific Entry by ID**
- **Endpoint**: `PUT /api/bloodbank/{id}`

#### **Delete a Specific Entry by ID**
- **Endpoint**: `DELETE /api/bloodbank/{id}`
- **Additional Notes**:
  - Returns `Not Found` if the ID is already deleted.

---

### **2. Pagination**
- **Endpoint**: `PUT /api/bloodbank/pagination`

---

### **3. Search Functionality**
- **Search for entries based on blood type**:
  - **Endpoint**: `GET /api/bloodbank/search?bloodType={bloodType}`
- **Search for entries based on status**:
  - **Endpoint**: `GET /api/bloodbank/search?status={status}`
- **Search for entries based on donor name**:
  - **Endpoint**: `GET /api/bloodbank/search?donorName={donorName}`

---

### **4. Sorting**
- **Sorting based on blood type**:
  - **Endpoint**: `GET /api/bloodbank/sortbloodtype`
- **Sorting based on collection date**:
  - **Endpoint**: `GET /api/bloodbank/sortcollectiondate`

---

### **5. Filtering**
- **Filtering based on blood type and status**:
  - **Endpoint**: `GET /api/bloodbank/filter`

### Images are there in the following documnetation 
- **https://drive.google.com/file/d/1jonlXqgmd6PPqO0-A8UNvggoBEPjBo4E/view?usp=sharing**