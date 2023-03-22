## How would we like to receive the solution to the test?
1. Clone this repository and upload it as a new public repository on a github account
2. Create a new branch in this repository
3. Create a pull request with the requested functionality to the unchanged master branch of the repository
4. Send us the PR link

## The API
This project includes an API that can be consumed but it is old and the controllers and models are not relevant to the business anymore. 

## The tasks
1. In this task you need to implement the necessary RESTful endpoints for our new library system. It principally implements the retrieval and storage of books and their authors. Check the following conceptual model as a starting guide:

[![](https://mermaid.ink/img/pako:eNp1kLFuAjEMhl8l8lhg6JqNE0uXExLd6g7uJXARiVMlzoAQ715zHBI6qRmi385vf7GvMGTnwcIQqdZdoFOhhDxFpsv5bK7IRs_qw1kTWOboM0j01lQpgU9Px6HrF6l9-4lhIAmZdyRa4PRGviE_GdsmYy7_UHpKS0g_NaMY5LJ46UKR8ZXyyOoM1U6jfH3P5JmJ8I5gNhsVbyruFmRYQ_IlUXC6lOlXCDL65BGsSueP1KIgaCu1UpN8uPAAVkrza2i_d_S8RrBHitXf_gDPunSA?type=png)](https://mermaid.live/edit#pako:eNp1kLFuAjEMhl8l8lhg6JqNE0uXExLd6g7uJXARiVMlzoAQ715zHBI6qRmi385vf7GvMGTnwcIQqdZdoFOhhDxFpsv5bK7IRs_qw1kTWOboM0j01lQpgU9Px6HrF6l9-4lhIAmZdyRa4PRGviE_GdsmYy7_UHpKS0g_NaMY5LJ46UKR8ZXyyOoM1U6jfH3P5JmJ8I5gNhsVbyruFmRYQ_IlUXC6lOlXCDL65BGsSueP1KIgaCu1UpN8uPAAVkrza2i_d_S8RrBHitXf_gDPunSA)

Implement the following endpoints:
   - POST /authors (add validation of fields)
   - POST /books (add validation of fields and correlate with an existing author)
   - GET /books/{id}
     - This endpoint should optionally retrieve the book's author's information in the same response.
   - GET /books (add filter by relevant properties)

Book data should be saveable and retrievable (but need not necessarily persist). Use the architecture you prefer and the structure you feel more comfortable with. You may be asked why you chose one solution over the other.

1. You can consider the existing code as legacy. Improve on it. Please focus on readability, maintainability and self-explanatory definitions.
2. **Optional** (nice to have) Add a test project. The use of more than one type of test (unit, functional, integration…) will count in your favor.
3. **Optional** (nice to have) Try to generate a Docker image for the project.
4. **Optional** (nice to have) Add swagger.
5. **Optional** (nice to have) Create an enpoint to connect to an external api to retrieve real book data.
6. **Optional** Do you want to go even further? Create an endpoint to retrieve a document with 3 million random generated models of Authors (.json formatted)

### What we will be looking at
- How do you refactor the existing code to make it more readable and structured?
- How do you implement new features and understand the requirements (feel free to ask us if in doubt)?
- How can we test the correctness of your solution?

### Restrictions
- Please do not modify the API contract.
- Besides changing/adding the legacy code, **everything** is allowed. Feel free to add new projects, libraries etc.; everything you consider to be useful, included and not restricted to: update framework and/or packages.
- If there is something that you want us to take note of in your approach, please add a text file describing it.
