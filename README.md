API Test Tool
This is an API Testing Tool developed in both Java and .NET to test and debug API endpoints. Users can select the request method (either GET or POST), input the API URL and request parameters, send the request, and view the response.

Project Background
I developed this tool while applying for the Software Test Engineer position to demonstrate my learning ability and hands-on skills. Even though .NET and Java are new technologies to me, I was able to quickly learn and apply them to build this project.

Features
Main Features:
API URL Input: Users can input the API URL they want to test.

Request Method Selection: Users can choose the request method (GET or POST).

Request Parameters Input: Users can enter request parameters in JSON format (for POST requests).

Send Request: Users can click a button to send the request.

Display Response: The response from the API will be displayed on the interface, and any JSON response will be formatted for easy reading.

Supported Request Methods:
GET: Used to retrieve data.

POST: Used to submit data.

Tech Stack
Java Version: Built with Swing or JavaFX for the GUI and using HttpClient for HTTP requests.

.NET Version: Built with WinForms for the desktop application and HttpClient for sending HTTP requests.

Installation and Usage
Java Version:
Ensure you have Java JDK 8 or higher installed.

Clone or download the project files:


git clone https://github.com/LYX-AI/Portfolio.git
Import into IDE (e.g., IntelliJ IDEA).

Run the ApiTester.java file to start using the tool.

.NET Version:
Ensure you have .NET Framework 4.7 or higher installed.

Clone or download the project files:


git clone https://github.com/LYX-AI/Portfolio.git
Open the ApiTester-DotNet folder in Visual Studio.

Run the ApiTester.cs file to start using the tool.

Usage:
After starting the application, input the API URL.

Select the request method (GET or POST).

Input the request parameters (for POST requests).

Click the Send Request button to view the response.

Sample Interface
Java Version Interface:
![image](https://github.com/user-attachments/assets/376809da-974f-4bfe-9699-f0410853809d)

.NET Version Interface:
![image](https://github.com/user-attachments/assets/ecc7fd09-772c-4c56-b557-d346be6ef5cf)


Project Structure:

![image](https://github.com/user-attachments/assets/3aee71db-b78e-47af-ae88-4ce077667e26)


![image](https://github.com/user-attachments/assets/a44aa7c3-8f48-4347-b6a8-988bab30eb16)


Learning Outcomes
During the development of this project, I learned and mastered the following technologies:

Java Programming: Applied JavaFX or Swing for building desktop GUI applications and used HttpClient for HTTP requests.

.NET Programming: Applied WinForms for building desktop applications and used HttpClient for HTTP requests.

API Testing: Implemented sending API requests and displaying responses, mastering the basics of API testing.

Future Improvement Plans
Add support for additional HTTP methods like PUT, DELETE.

Add features like request headers, custom validation to improve flexibility.

Enhance the user interface to make it more user-friendly.

Support file uploads and different request formats.
-------------------------------------------------------------------------------------------------------------
## 🔹 ApiTester-JS: Automated API Testing Harness

<!-- Add this section to your main README.md -->

### Project Overview

`ApiTester-JS` is a lightweight, TypeScript-powered API testing framework built with Jest and Axios. It offers:

* **JSON-based Test Configuration**: Define each API test in `tests.json` with the endpoint URL, HTTP method, expected status code, and optional data length checks.
* **Command-Line Testing**: Run `npm test` to execute all test cases and view PASS/FAIL results in your terminal.
* **Browser-Based UI**: Launch a simple web interface with `npm start`, navigate to [http://localhost:3000](http://localhost:3000), and click **Run Tests** to see formatted JSON reports.
* **Continuous Integration**: Integrated with GitHub Actions (`.github/workflows/ci.yml`) to automatically run tests on every push or pull request.

### Quick Start

1. **Clone the repository**

   ```bash
   git clone https://github.com/LYX-AI/Portfolio.git
   cd ApiTester-JS
   ```
2. **Install dependencies**

   ```bash
   npm install
   ```
3. **Run CLI tests**

   ```bash
   npm test
   ```
4. **Launch Web UI**

   ```bash
   npm start
   ```

   Open your browser at [http://localhost:3000](http://localhost:3000) and click **Run Tests**.

### Directory Structure

```
ApiTester-JS/
├── public/               # Static frontend files
│   └── index.html        # Web UI entry point
├── src/
│   ├── server.ts         # Express server and test trigger endpoint
│   └── testRunner.test.ts# Jest-based test runner
├── tests.json            # JSON test case definitions
├── package.json          # npm scripts & dependencies
├── tsconfig.json         # TypeScript configuration
└── .github/
    └── workflows/        # GitHub Actions workflows
        └── ci.yml        # CI configuration
```

### Continuous Integration (CI)

![CI Status](https://github.com/LYX-AI/Portfolio.git/actions/workflows/ci.yml/badge.svg)

* **Trigger**: On push or pull request to `main`/`master`, changes in `ApiTester-JS/**` or workflow file.
* **Steps**: Checkout → Setup Node.js → `npm ci` → `npm test`.







