# 3420 Assignment #4 - Winter 2023

Name(s): SAMAKSH MONGA

Live Loki link(s):

## Rubric

| Component                                                    | Grade |
| :----------------------------------------------------------- | ----: |
| Add Book Validation                                          |    /5 |
| Create Account Validation                                    |    /5 |
| Delete confirmation                                          |    /3 |
| Details modal                                                |    /3 |
|                                                              |       |
| Collapsible Nav                                              |    /3 |
| Unique Username                                              |    /3 |
| Password Strength                                            |    /3 |
| Show Password                                                |    /3 |
| Summary Limit                                                |    /3 |
| Star Rating                                                  |    /3 |
|                                                              |       |
| Code Quality (tidyness, validity, efficiency, etc)           |    /4 |
| Documentation                                                |    /3 |
| Testing                                                      |    /3 |
|                                                              |       |
| Bonus                                                        |    /2 |
| Deductions (readability, submission guidelines, originality) |       |
|                                                              |       |
| Total                                                        |   /35 |

## Table of Contents

## Things to consider for Bonus Marks (if any)

## Code & Testing

### REGISTER PAGE

###REGISTER PHP AND HTML
```xml
<?php
session_start();


require "includes/library.php";

$pdo = connectDB();

$errors = array();

$username = $_POST['username'] ?? null;
$first_name = $_POST['first_name'] ?? null;
$email = $_POST['email'] ?? null;
$password = $_POST['password'] ?? null;
$last_name = $_POST['last_name'] ?? null;
$cpassword = $_POST['cpassword'] ?? null;
if (isset($_POST['submit'])) {

    // // Validate username
    // if (!isset($username) || strlen($username) < 3 || !ctype_alnum($username)) {
    //     $errors['username'] = true;
    // } else {
    //     // Check if username is unique
    //     $stmt = $pdo->prepare("SELECT COUNT(*) FROM cois3420_Assignment3_users WHERE username = ?");
    //     $stmt->execute([$username]);
    //     if ($stmt->fetchColumn() > 0) {
    //         $errors['username'] = true;
    //     }
    // }

    // Validate first name
    if (!isset($first_name) || strlen($first_name) < 2 || !ctype_alpha($first_name)) {
        $errors['first_name'] = true;
    }

    // Validate last name
    if (!isset($last_name) || strlen($last_name) < 2 || !ctype_alpha($last_name)) {
        $errors['last_name'] = true;
    }

    // // Validate email
    // if (!isset($email) || !filter_var($email, FILTER_VALIDATE_EMAIL)) {
    //     $errors['email'] = true;
    // }

    // Validate password
    if (!isset($password) || strlen($password) < 8 || !preg_match('/\d/', $password) || !preg_match('/[A-Z]/', $password)) {
        $errors['password'] = true;
    }

    // Validate password
    if ($password !== $cpassword) {
        $errors['passwordmismatch'] = true;
      }

    // Only do this if there weren't any errors
    if (count($errors) === 0) {
        // Hash the password
        $hashed_password = password_hash($password, PASSWORD_DEFAULT);

        // Insert the record into the database
        $query="insert into cois3420_Assignment3_users VALUES (NULL,?,?,?,?,?,NULL)";
        $stmt = $pdo->prepare($query);
        $stmt->execute([$first_name, $last_name, $email, $username, $hashed_password]);

        // Send the user to the thank you page
        header("Location:thanks_register.php");
        exit();
    }
}
?>
```
```xml

<!--starting up with th html-->
<!DOCTYPE html>
<html lang="en">
    <head>
        <?php $page_title = "Register"; include 'includes/metadata.php'; ?>
        <script  defer src="java/register.js"></script>
    </head>
    <body>
            <?php include 'includes/header.php'; ?>

            <section>
                <h3>Create Account</h3>

                <form method="POST" id='requestform'>
                    <div>
                        <label for="username">Username</label><!--getting the username-->
                        <input type="text" id="username" name="username" placeholder="Username" value="<?= $username ?>" required>
                        <span class="error" style="display: none;">Please enter a valid username of atlaest 3 characters, or choose a different one</span>
                    </div>

                    <div>
                        <label for="first_name">First Name</label><!--getting the first name-->
                        <input type="text" id="first_name" name="first_name" placeholder="First Name" value="<?= $first_name ?>" required>
                        <span class="error <?=!isset($errors['first_name']) ? 'hidden' : "";?>">Please enter correct First name</span>
                    </div>
                    <div>
                    <label for="last_name">Last Name</label><!--getting the last name-->
                    <input type="text" id="last_name" name="last_name" placeholder="Last Name" value="<?= $last_name ?>" required>
                    <span class="error <?=!isset($errors['last_name']) ? 'hidden' : "";?>">Please enter a valid last name (alphabetical and at least 2 characters)</span>
                    </div>

                    <div>
                        <label for="email">Email</label><!--getting the email-->
                        <input type="email" id="email" name="email" placeholder="Email" value="<?= $email ?>" required>
                        <span class="error <?=!isset($errors['email']) ? 'hidden' : "";?>">Please enter a valid email address</span>
                    </div>

                     
                    <div class="input-box"> 
                        <label for="password">Password</label> 
                        
                        <input type="password" id="password" name="password" placeholder="Password" required>
                        <p id="message">Password is <span id="strength"></span></p> 
                        
                        
                    </div>
                   
                    <div>    
                    
                    
                        Password must contain:
                        <ul>
                            <li>At least 8 characters</li>
                            <li>At least one uppercase letter</li>
                            <li>At least one numeral</li>
                            <li>At least one special character (!, #, $, etc)</li>
                        </ul>
                    </div>
                    
                    <div>
                        <label for="password1">Retype Password: </label><!--verifying the password-->
                        <input type="password" name="cpassword" id="cpassword" placeholder="Retype Password" />
                        <span class="errorform <?= !isset($errors['passwordmismatch']) ? 'hidden' : "" ?>">Password Do not match</span>
                    </div>

                    <div>
                        <button type="submit" name="submit">Create Account</button><!--submitting and adding data to the user-->
                    </div>
                </form>
        </section>

        <?php include 'includes/footer.php'; ?>
    </body>
</html>
<!--ending of html-->
```

'''XHR
"use strict";

// Wait for the DOM to load before executing JavaScript
window.addEventListener("DOMContentLoaded", () => {

    // Get references to the password input field and the message and strength elements
    var pass = document.getElementById("password");
    var msg = document.getElementById("message");
    var str = document.getElementById("strength");

    // Listen for input events on the password field
    pass.addEventListener('input', () => {
        // Show the message element if the password field has a value
        if(pass.value.length > 0){
            msg.style.display = "block";
        } 
        else{
            msg.style.display = "none";
        }
        // Check the length of the password and set the strength and message accordingly
        if(pass.value.length < 4){
            str.innerHTML = "Very Weak";
            pass.style.borderColor = "red";
            msg.style.color = "red";
        }
        else if(pass.value.length >= 4 && pass.value.length <8){
            str.innerHTML = "Medium";
            pass.style.borderColor = "#9B8000";
            msg.style.color = "#9B8000";
        }
        else if(pass.value.length >= 8){
            str.innerHTML = "STRONG";
            pass.style.borderColor = "green";
            msg.style.color = "green";
        }
    })

    // Get reference to the username input field
    const username = document.getElementById("username");

    // Listen for change events on the username field
    username.addEventListener("submit", () => {
        // Remove any previous quantity message
        const qtySpan = document.getElementById('qty');
        if (qtySpan) {
            qtySpan.remove();
        }
        // Send an AJAX request to check if the username already exists
        const xhr = new XMLHttpRequest();
        xhr.open("GET", `checkusername.php?username=${username.value}`);
        xhr.addEventListener("load", () => {
            if (xhr.status === 200) {
                if (xhr.responseText === 'true') {
                    // Add a message indicating that the username already exists
                    username.insertAdjacentHTML("afterend", "<span id='qty'>Username already exists, please enter a different username</span>");
                } else if (xhr.responseText === "false") {
                    // Add a message indicating that the username is available
                    username.insertAdjacentHTML("afterend", "<span id='qty'>Username is available</span>");
                }
            } else {
                // Add a message indicating that a username needs to be entered
                username.insertAdjacentHTML("afterend", "<span id='qty'>Please enter a username</span>");
            }
        });
        xhr.send();
    });

    // Get reference to the request form
    const requestForm = document.getElementById("requestform");

    // Listen for submit events on the form
    requestForm.addEventListener("submit", (ev) => {
        // Prevent the form from submitting by default
        ev.preventDefault();
        // Call the validateInputs function to validate the input fields
        validateInputs();
    });
});

### Testing
<img src="D:\Winter 2022\COIS3420\Loki\3420\assignments\assn4\img\REGISTER.png">



###details
```php
function myFunction(){
<?php
//inclusing the library
require_once 'includes/library.php';
$pdo = connectDB();//connecting to the database with the help of library
session_start();
if(!isset($_SESSION['username']))//checking if the user is logged in
{
 header("Location:login.php");//otherwise sending the user to login
 exit();
}
$x=0;
$source1="https://loki.trentu.ca/~samakshmonga/www_data/";//letting the php know the path for the book cover
$query= "select * from cois3420_Assignment3_alldata";//selecting all the data 
$stmt = $pdo->prepare($query);//preparing the query


//if there is a record, then x=1 otherwise x=2
if(!$stmt->fetch()) //fetching if there is any record otherwise 
{
    $x=1;
    
}
else
{
    $x=2;
}
$user=$_SESSION['username'];

?>
}
```
```xml


<!--starting eith html-->
<!DOCTYPE html>
<html lang="en">
    <head>
    <?php
    $page_title = "Main_Page";
    include 'includes/metadata.php'; ?>
    </head>
    <body>
        <?php include 'includes/header.php'; ?>
        <script defer src="java/details.js"></script>
        <section>
            <main>
                <div>
                
                <h3> BOOK DETAILS FOR:  <?php echo $_SESSION['username']?></h3><!-- echoing out the username for the particular session-->
                    
                    <form id="adding picture" enctype="multipart/form-data" action="<?= htmlentities($_SERVER['PHP_SELF']); ?>" method="post"   novalidate>
                        <?php if($x==1):?>
                        <?php
                        //selecting every detail of the particular book
                        $query="select * from cois3420_Assignment3_alldata WHERE username=? and id=?";
                        $stmt = $pdo->prepare($query);
                        $stmt->execute([$_SESSION['username'], $_GET['id']]);
                        
                        ?>
                        
                        <div>
                            <?php foreach ($stmt as $row): ?>
                            
                            <?php $source2=$source1.$row['Book_Cover']?>
                            <img src="<?= $source2 ?>" alt="" height="260"
                            width="190">
                            <!-- displaying the data for the particular book-->
                            <div>
                                <ul>

                                    <ul><h3>Book Title: <?= $row['Book_Title'] ?></h3>
                                    <li><h3>Book Genre: <?= $row['Genre'] ?></h3>
                                    <li><h3>Book url: <?= $row['Book_url'] ?></h3>
                                    <li><h3>Author: <?= $row['Author'] ?></h3>
                                    <li><h3>ISBN: <?= $row['ISBN'] ?></h3>
                                    <li><h3>pages: <?= $row['pages'] ?></h3>
                                    <li><h3>Description: <?= $row['Description'] ?></h3>
                                    <li><h3>Publish_date: <?= $row['Publish_date'] ?></h3>
                                    <li><h3>entry_date: <?= $row['entry_date'] ?></h3>
                                </ul>
                            </div>
                        
                            <?php endforeach; ?>
                            <?php elseif($x==2):?>
                            <div>
                                <!--otherwise adding a new book-->
                                <h3>Add a Book</h3>
                                <a href ="addbook.php"><button id="addbook" name="login"
                                class="button" type="button">Add A New Book</button></a>
                            </div>
                            <?php endif?>
                        </div>
                    </form>
                </div>
            </main>
        </section>
    </body>
</html>
<!--ending of html-->
```
```
function myFunction(){
    var bookIcon = document.getElementById("book-icon");

    
    
    

    // Add a click event listener to the book icon
    bookIcon.addEventListener('Details for this book', async () => {
    try {
        // Load the book details using AJAX
        const response = await fetch('/details.php?id=123');
        const data = await response.json();

        // Display the book details in the modal
        modal.innerHTML = `<h1>${data.title}</h1><p>${data.description}</p>`;
        modal.style.display = 'block';
    } catch (error) {
        // Handle any errors that might occur
        console.error('Error loading book details:', error);
        alert('There was an error loading the book details. Please try again later.');
    }
    });

    
    };

```


<img src="D:\Winter 2022\COIS3420\Loki\3420\assignments\assn4\img\DETAILSS.png">

###addbook
```php
<?php
/****************************************
// ENSURES THE USER HAS ACTUALLY LOGGED IN
// IF NOT REDIRECT TO THE LOGIN PAGE HERE
******************************************/
session_start();//starting my session
//checking if the user is logged in or not
if (!isset($_SESSION['username'])) {
   header("Location: login.php");
   exit();
}
//including the library function
require "includes/library.php";

// CONNECT TO DATABASE
$pdo = connectDB();

$errors = array(); //declare empty array to add errors too

//get values from post or set to NULL if doesn't exist
$Book_Cover = $_POST['Book_Cover'] ?? null;
$Book_Title = $_POST['Book_Title'] ?? null;
$Book_url=$_POST['Book_url']?? null;
$Genre = $_POST['Genre'] ?? null;
$Author = $_POST['Author'] ?? null;
$Description = $_POST['Description'] ?? null;
$ISBN = $_POST['ISBN'] ?? null;
$pages = $_POST['pages'] ?? null;
$Publish_date=$_POST['Publish_date']?? null;



if (isset($_POST['submit'])) { //only do this code if the form has been submitted
    // if ($_FILES['filename']['error']!= 4) {
    //     //calling library function to check errors
    //     $generate=checkErrors('filename',2400000);
    //     if (strlen($generate)>0){//checking the string length
    //         $errors['Book_Cover'] = $generate;
    //     }
    // }

    // //checking the length of book title
    // if (!isset($Book_Title) || strlen($Book_Title) === 0) {
    //     $errors['Book_Title'] = true;
    // }

    // //checking length of my url
    // if (!isset($Book_url) || strlen($Book_url) === 0) {
    //     $errors['Book_url'] = true;
    // }

    // //checking length og genre
    // if (!isset($Genre) || strlen($Genre) === 0) {
    //     $errors['Genre'] = true;
    // }
    // //checking length of author
    // if (!isset($Author) || strlen($Author) === 0) {
    //     $errors['Author'] = true;
    // }

    // //using strip tag so that if there are any tags it should just uplaod the body content
    // $Description = strip_tags($Description);

    // //validate user has entered ISBN
    // if (!isset($ISBN) || strlen($ISBN) === 0 || !is_int((int) $ISBN)) {
    //     $errors['ISBN'] = true;
    // }
    // //validate user has entered a PAGES
    // if (!isset($pages) || strlen($pages) === 0 || !is_int((int) $pages)) {
    //     $errors['pages'] = true;
    // }
    // //VALIDATING THAT USER ENTERED THE DATE 

    if (!isset($Publish_date) || date($Publish_date) === 0 ) {
        $errors['Publish_date'] = true;
    }

    //only do this if there weren't any errors
    if (count($errors) === 0) {
       
        $query = "insert into cois3420_Assignment3_alldata values (NULL,?,?,?,?,?,?,?,?,?,?,NOW())"; //SQL QUERY TO INSERT INTO TABLE IN MY DATABASE
        $stmt = $pdo->prepare($query);//PREPARING MY QUERY
        $stmt->execute([$_SESSION['username'], $Book_Cover, $Book_Title, $Book_url, $Genre, $Author, $Description,(int)$ISBN, (int)$pages, $Publish_date]);//PASSING THE FIELD VALUES
        $book_id=$pdo->lastInsertId();//TAKING THE LAST ID IN THE USER
        $filekey = 'filename';//SETTING UP THE FILE KEY
        $path = WEBROOT . "www_data/";//LETTING THE CODE KNOW THE PATH WHERE FILES SHOULD BE UPLOADED
        $filename = "cover";//SETTING UP THE COVER AS THE FILE NAME AS THE PREFIX OF MY IMAGE NAME 
        $uniqueID=$book_id;//GETTING BOOK ID AS UNIQUE ID
        if (is_uploaded_file($_FILES['filename']['tmp_name'])) {
            $results = checkErrors($filekey, 2400000);//SETTING UP THE MAX VALUE AS WELL AS THE FILENAME OF MY FILE
            if (strlen($results) > 0) {//CHECKING IF THE LENGTH OF THE RESULTS ARE ZERO
                $errors['file'] = $results; //this should be handled more gracefully
            } else {
                $newname = createFilename($filekey,$filename, $uniqueID);//PASSING NEW NAME AS THE FUNCTION IN LIBRARY.PHP
                if (!move_uploaded_file($_FILES[$filekey]['tmp_name'], $path.$newname)) {
                    $errors['file'] = "Failed to move your file"; //this should be handled more gracefully
                } else {
                    $query="update cois3420_Assignment3_alldata SET Book_Cover=? where id=?";//UPDATING THE TABLE WITH THE BOOK COVER NAME IN IT
                    $stmt=$pdo->prepare($query);//PREPARING THE QUERY
                    $stmt->execute([$newname,$book_id]);//PASSING THE ATTRIBUTES INTO SQL QUERY
                }
            }
        } else {
            $errors['file'] = checkErrors($filekey, 2400000);//SETTING THE ERRORS SATEMENT
        }
        header("Location:thanks_addbook.php");//REEDIRECTING USER INTO ADD BOOK
        exit();//EXITING 
    }

}
?>
```
```xml
<!-- STARTING THE HTML -->
<!DOCTYPE html>
<html lang="en">

<head>
    <script defer src="java/addbook.js"></Script>
    <?php
    $page_title = "Add_Book";//SEETING UP THE PAGE TITLE
    include 'includes/metadata.php'; ?><!-- INCLUDING THE META LIBRARY-->
</head>

<body>
    <!-- INCLUDING THE HEADER FILE -->
    <?php include 'includes/header.php'; ?>
    <section>

        <h2>Add your new book</h2><!--//HEADING TO THE FILE -->
        <form id="requestform" enctype="multipart/form-data" action="<?= htmlentities($_SERVER['PHP_SELF']); ?>" method="post"
            novalidate>
            <!--CREATING SEPERATE DIV FOR EVERY ENTERED FIELD-->
            <div>
                <label for="enterfile">Upload Cover:</label>
            
                <input type="file" id="enterfile" name="filename">
                <span class="error <?= !isset($errors['Book_Cover']) ? 'hidden' : "" ?>"><?php echo $errors['Book_Cover']?></span><!--DISPLAYING THE ERRORS-->
                
            </div>
            

            <div>
                <label for="Book_Title"> Book Title:</label>
                <input type="text" id="Book_Title" name="Book Title" placeholder="Title" value="<?= $Book_Title ?>" />
                <span class="error <?= !isset($errors['Book_Title']) ? 'hidden' : ""; ?>">Please enter correct book
                    title</span><!--DISPLAYING THE ERRORS-->
            </div>
            <div>
                <label for="Book_url"> Book Url:</label>
                <input type="url" id="Book_url" name="Book url" placeholder="Book_url" value="<?= $Book_url ?>" />
                <span class="error <?= !isset($errors['Book_url']) ? 'hidden' : ""; ?>">Please enter correct book
                    url</span><!--DISPLAYING THE ERRORS-->
            </div>
            <div>
                <label for="Genre"> Genre</label>
                <input type="text" id="Genre" name="Genre" placeholder="Genre" value="<?= $Genre ?>" />
                <span class="error <?= !isset($errors['Genre']) ? 'hidden' : ""; ?>">Please enter correct Genre of
                    book</span><!--DISPLAYING THE ERRORS-->
            </div>
            <div>
                <label for="Author">Author</label>
                <input type="text" id="Author" name="Author" placeholder="Author" value="<?= $Author ?>" />
                <span class="error <?= !isset($errors['Author']) ? 'hidden' : ""; ?>">Please enter correct Author of
                    book</span><!--DISPLAYING THE ERRORS-->
            </div>
            <div>
                <label for="Description">Description:</label>
                <textarea id="Description" name="Description" rows="12" cols="20" value="<?= $Description ?>" placeholder="Book Description" maxlength="2500"></textarea>
                <span class="error" style="display: none;">Please enter a book description.</span>
                <!-- Outputting error message -->
                <div id="plot-summary-counter">2500 characters left</div>
            </div>

            <div>
                <label for="ISBN"> ISBN</label>
                <input type="number" id="ISBN" name="ISBN" placeholder="ISBN" value="<?= $ISBN ?>" />
                <span class="error <?= !isset($errors['ISBN']) ? 'hidden' : ""; ?>">Please enter correct ISBN of
                    book</span><!--DISPLAYING THE ERRORS-->
            </div>

            <div>
                <label for="pages"> Number of Pages</label>
                <input type="number" id="pages" name="pages" placeholder="Number of Pages" value="<?= $pages ?>" />
                <span class="error <?= !isset($errors['pages']) ? 'hidden' : ""; ?>">Please enter correct pages of
                    book</span><!--DISPLAYING THE ERRORS-->
            </div>
            <div>
                <label for="Publish_date"> Publication_Date</label>
                <input type="date" id="Publish_date" name="Publish_date" placeholder="Publish_date" value="<?= $Publish_date ?>" />
                <span class="error <?= !isset($errors['Publish_date']) ? 'hidden' : ""; ?>">Please enter correct Publish_Date </span><!--DISPLAYING THE ERRORS-->
            </div>
            <div>
                <button type="reset" name="Cear Form" id="reset"> Clear Form</button><!--RESETING THE FORM SO MAKE IT CLEAR IN ALL FIELDS-->
                
            </div>
            <div class="Display">
            <button id="submit" name="submit" >Add Book</button></div>              <!--SUBMITTING THE FORM-->
            </div>
        </form>
    </section>
    </main>
    <?php include 'includes/footer.php'; ?><!--INCLUSIND THE FOOTER FILE-->
</body>

</html>
```
```
"use strict";

window.addEventListener("DOMContentLoaded", () => {
  // Description plot checker
  let plotSummaryField = document.getElementById("Description");
  let plotSummaryCounter = document.getElementById("plot-summary-counter");
  plotSummaryField.addEventListener("input", function () {
    let remainingChars =
      plotSummaryField.maxLength - plotSummaryField.value.length;
    plotSummaryCounter.textContent = remainingChars + " characters left";
  });

  const form = document.getElementById("requestform");
  const Book_Title = document.getElementById("Book_Title");
  const Author = document.getElementById("Author");
  const Description = document.getElementById("Description");
  const Publish_date = document.getElementById("Publish_date");
  const ISBN = document.getElementById("ISBN");
  const pages = document.getElementsByName("pages")[0];
  const Genre = document.getElementById("Genre");

  // Add error message containers for each field
  const Book_TitleError = createErrorElement("title-error");
  const AuthorError = createErrorElement("author-error");
  const DescriptionError = createErrorElement("description-error");
  const ISBNError = createErrorElement("isbn-error");
  const pagesError = createErrorElement("pagesError");
  const Publish_dateError = createErrorElement("pub-date-error");
  Book_Title.parentNode.appendChild(Book_TitleError);
  Author.parentNode.appendChild(AuthorError);
  Description.parentNode.appendChild(DescriptionError);
  ISBN.parentNode.appendChild(ISBNError);
  pages.parentNode.appendChild(pagesError);
  Publish_date.parentNode.appendChild(Publish_dateError);

  function createErrorElement(id) {
    const errorElement = document.createElement("div");
    errorElement.id = id;
    errorElement.className = "error";
    errorElement.style.color = "red";
    return errorElement;
  }

  function showError(element, message) {
    element.textContent = message;
    element.parentNode.classList.add("has-error");
  }

  function hideError(element) {
    element.textContent = "";
    element.parentNode.classList.remove("has-error");
  }

  function isValidDate(d) {
    if (Object.prototype.toString.call(d) === "[object Date]") {
      return !isNaN(d.getTime());
    }
    return false;
  }

  form.addEventListener("submit", function (event) {
    let hasError = false;

    // validating book title
    if (!Book_Title.value.trim()) {
      showError(Book_TitleError, "Please enter a book title.");
      hasError = true;
    } else {
      hideError(Book_TitleError);
    }

    // validating book author
    if (!Author.value.trim()) {
      showError(AuthorError, "Please enter an author.");
      hasError = true;
    } else {
      hideError(AuthorError);
    }

    // validating book description
    if (!Description.value.trim()) {
      showError(DescriptionError, "Please enter a description.");
      hasError = true;
    } else {
      hideError(DescriptionError);
    }

    // validating book publish date
    const dateRegEx = /^\d{2}-\d{2}-\d{4}$/;
    if (!Publish_date.value.match(dateRegEx)) {
      showError(
        Publish_dateError,
        "Please enter a valid date format (MM-DD-YYYY)."
      );
      hasError = true;
    } else {
      const [month, day, year] = Publish_date.value.split("-");
      const parsedDate = new Date(`${month}-${day}-${year}`);
      if (!isValidDate(parsedDate)) {
        showError(Publish_dateError, "Please enter a valid date.");
        hasError = true;
      } else {
        hideError(Publish_dateError);
      }
    }

    // validating book ISBN
    if (!ISBN.value) {
      showError(ISBNError, "Please enter ISBN ");
      hasError = true;
    } else if (ISBN.value.length < 10 || ISBN.value.length > 13) {
      showError(ISBNError, "ISBN must be between 10 and 13 characters.");
      hasError = true;
    } else if (!/^\d+$/.test(ISBN.value)) {
      showError(ISBNError, "ISBN can only contain numbers.");
      hasError = true;
    } else {
      hideError(ISBNError);
      // Check ISBN validity
      let isbn = ISBN.value;
      // Remove any dashes or spaces from the input
      isbn = isbn.replace(/[- ]/g,'');
      // Check if ISBN-13
      if (isbn.length === 13) {
        let sum = 0;
        for (let i = 0; i < 12; i++) {
          sum += parseInt(isbn[i]) * (i % 2 === 0 ? 1 : 3);
        }
        let checkDigit = (10 - (sum % 10)) % 10;
        if (checkDigit !== parseInt(isbn[12])) {
          showError(ISBNError, "Invalid ISBN-13 check digit.");
          hasError = true;
        }
      // Check if ISBN-10
      } else if (isbn.length === 10) {
        let sum = 0;
        for (let i = 0; i < 9; i++) {
          sum += parseInt(isbn[i]) * (10 - i);
        }
        let checkDigit = (11 - (sum % 11)) % 11;
        if (checkDigit !== parseInt(isbn[9])) {
          showError(ISBNError, "Invalid ISBN-10 check digit.");
          hasError = true;
        }
      } else {
        showError(ISBNError, "Invalid ISBN length.");
        hasError = true;
      }
    }
  });
})


```
<img src="D:\Winter 2022\COIS3420\Loki\3420\assignments\assn4\img\DESCRIPTION.png">


###deletebook
```php
<?php
//code for deleting a particular book for his or her account
require_once 'includes/library.php';
session_start();//starting up my session for this page
if (!isset($_SESSION['username'])) {
    header("Location:login.php");//other wise redirecting towards the login.php
    exit();
}

$pdo = connectDB();//connecting to the databse

$Book_title = $_POST['Book_title'] ?? null;//setting up the book title

if (isset($_POST['delbook'])) {

    if (strlen($Book_title) !== 0) {

        $stmt = $pdo->prepare("DELETE FROM cois3420_Assignment3_alldata WHERE username = ? and Book_title = ?");// deleting that particular book 
        $stmt->execute([$_SESSION['username'], $Book_title]);
        

    
    //redirecting after insertion
    header("Location:thanks_delbook.php");//redirecting towards the del book
    exit();
}

    else {
    printf("Error occured");// otherwise printing an error
}
}




?>
```
```xml
<!-- starting up the html-->
<!DOCTYPE html>
<html lang="en">

<head>
    <?php $pagetitle = 'Deletebook' ?>
    <?php include 'includes/metadata.php' ?>
    <script defer src="java/deletebook.js"></script>
</head>

<body>
    <?php include 'includes/header.php'; ?>
    <main>
        <section>

            <h2>Delete a book</h2>
            <form id="delbook" enctype="multipart/form-data" action="<?= htmlentities($_SERVER['PHP_SELF']); ?>"
                method="post" novalidate>
                <div>

                        <label for="Book_title"> Book Title</label><!--getting the book title-->
                        <input type="text" id="Book_title" name="Book_title" placeholder="Book Title" />
                        <span class="error <?= !isset($errors['Book_title']) ? 'hidden' : ""; ?>">Please enter book title

                </div>
                <div class="Display">
                    <button id="delbook" name="delbook" type="submit"> Delete Book</button><!-- deleting the book after two factor authentication-->
                </div>


            </form>
        </section>
    </main>
    <?php include 'includes/footer.php'; ?>
</body>

</html>
<div> 
                    <div class="input-box"> 
                        <label for="password">Password:</label> 
                        <input type="password" id="password" 
name="password" placeholder="Enter Password" required><br> 
                        <span class="error" style="display: none;">Please 
enter a Password</span> <!-- Outputting error message --> 
                        <p id="message">Password is <span id="strength">
</span></p>     
                    </div> 
```
```
// Use strict mode to enforce better coding practices
"use strict";
// Add a click event listener to the delete button
document.querySelector("#delbook").addEventListener("click", function(event) {
    // Ask the user to confirm that they want to delete the book
    if (!confirm("Are you sure you want to delete the book?")) {
        event.preventDefault();
        return;
    }
    // Create a new XMLHttpRequest object

    var xhr = new XMLHttpRequest();
    // Specify the HTTP request method and URL to send a request to
    xhr.open("GET", "deletebook.php?Book_title=" + document.querySelector("#Book_title").value);
    // Define a callback function to handle the server's response to the request
    xhr.onreadystatechange = function() {
        // Check if the request is done and the server has responded successfully
        if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
            // Display a success message to the user
            alert("Book has been deleted");
        }
    };
    // Send the HTTP request to the server
    xhr.send();
});
```

<img src="D:\Winter 2022\COIS3420\Loki\3420\assignments\assn4\img\DELETEBOOKS.png">
<img src="D:\Winter 2022\COIS3420\Loki\3420\assignments\assn4\img\DELETEBOOKS1.png">
<img src="D:\Winter 2022\COIS3420\Loki\3420\assignments\assn4\img\DELETEBOOKS2.png">


###log in page
```php
<?php
$username = $_POST['username'] ?? null; //getting teh userename and the password
$password = $_POST['password'] ?? null;

$errors = [];//setting up an empty array

if (isset($_POST['submit'])) {
    require_once 'includes/library.php';//including the library for the directory path
    $pdo = connectDB();//connecting to the database

    $query = $pdo->prepare('SELECT * FROM cois3420_Assignment3_users WHERE username = ?');//fetching all of the data to work with
    $query->execute([$username]);
    $user = $query->fetch();

    if (!$user) {
        $errors['user'] = true;//if user in not logged in
    } else {
        if (password_verify($password, $user['password'])) {
            session_start();//starting up the session
            $_SESSION['username'] = $username;
            $_SESSION['user_id'] = $user['id'];
            header('Location: index.php');
            exit();
        } else {
            $errors['login'] = true;//[assing it through log in page
        }
    }
}
?>
```
```xml
<!--starting up with the html-->
<!DOCTYPE html>
<html lang="en">

<head>
<?php
$page_title = "Assignment_3";
include 'includes/metadata.php'; ?>
<script defer src="java/showpass.js"></script>
</head>
<header>
  <div>
    <h1>
      Online Book Service
    </h1>
  </div>
</header>
<body>

    <!--  include 'includes/header.php'; ?> -->
    <main>
        <section>
        
        <h1> LOGIN PAGE </h1>
            <h2>
                Enter Your Details
            </h2>
            <form action="<?=htmlentities($_SERVER['PHP_SELF'])?>" method="POST" autocomplete="off">
                <div>
                    <label for="Username">Username</label>
                    <input type="text" 
                    id="Username" 
                    name="username" 
                    placeholder="Username" required><!--getting the username-->
                </div>
                <div>
                    <label for="Password">Password</label>
                    <input type="password" id="show" name="password" value="password" placeholder="Password" required /><!--getting the password-->
                    <input type="checkbox" onclick="myFunction()"/>
                </div>
                
                <div>
                    <span class="<?=!isset($errors['user']) ? 'hidden' : "";?>">*That user doesn't exist</span>
                    <!-- <span class="<?=!isset($errors['login']) ? 'hidden' : "";?>">*Incorrect login info</span> -->
                </div>
            <div class="kit">
                    <nav>
                        <a href="register.php"> Create Account</a><!--general links for redirecting to different useful pages-->
                        <a href="forgot.php"> Forgot Password </a>
                    </nav>
                </div>
                <button id="submit" name="submit">Log In</button><!--log in button-->
            </form>
        </section>
    </main>
    <!-- FOOTER-->
    <?php include 'includes/footer.php';?>
</body>

</html>
<!--html ending-->
```
<img src="D:\Winter 2022\COIS3420\Loki\3420\assignments\assn4\img\LOGINPAGE.png">



###BONUS MARK

### I have created an interactive java script for username verification directly from my database

```
"use strict";

// Wait for the DOM to load before executing JavaScript
window.addEventListener("DOMContentLoaded", () => {

    // Get references to the password input field and the message and strength elements
    var pass = document.getElementById("password");
    var msg = document.getElementById("message");
    var str = document.getElementById("strength");

    // Listen for input events on the password field
    pass.addEventListener('input', () => {
        // Show the message element if the password field has a value
        if(pass.value.length > 0){
            msg.style.display = "block";
        } 
        else{
            msg.style.display = "none";
        }
        // Check the length of the password and set the strength and message accordingly
        if(pass.value.length < 4){
            str.innerHTML = "Very Weak";
            pass.style.borderColor = "red";
            msg.style.color = "red";
        }
        else if(pass.value.length >= 4 && pass.value.length <8){
            str.innerHTML = "Medium";
            pass.style.borderColor = "#9B8000";
            msg.style.color = "#9B8000";
        }
        else if(pass.value.length >= 8){
            str.innerHTML = "STRONG";
            pass.style.borderColor = "green";
            msg.style.color = "green";
        }
    })

    // Get reference to the username input field
    const username = document.getElementById("username");

    // Listen for change events on the username field
    username.addEventListener("submit", () => {
        // Remove any previous quantity message
        const qtySpan = document.getElementById('qty');
        if (qtySpan) {
            qtySpan.remove();
        }
        // Send an AJAX request to check if the username already exists
        const xhr = new XMLHttpRequest();
        xhr.open("GET", `checkusername.php?username=${username.value}`);
        xhr.addEventListener("load", () => {
            if (xhr.status === 200) {
                if (xhr.responseText === 'true') {
                    // Add a message indicating that the username already exists
                    username.insertAdjacentHTML("afterend", "<span id='qty'>Username already exists, please enter a different username</span>");
                } else if (xhr.responseText === "false") {
                    // Add a message indicating that the username is available
                    username.insertAdjacentHTML("afterend", "<span id='qty'>Username is available</span>");
                }
            } else {
                // Add a message indicating that a username needs to be entered
                username.insertAdjacentHTML("afterend", "<span id='qty'>Please enter a username</span>");
            }
        });
        xhr.send();
    });

    // Get reference to the request form
    const requestForm = document.getElementById("requestform");

    // Listen for submit events on the form
    requestForm.addEventListener("submit", (ev) => {
        // Prevent the form from submitting by default
        ev.preventDefault();
        // Call the validateInputs function to validate the input fields
        validateInputs();
    });
});
```
<img src="D:\Winter 2022\COIS3420\Loki\3420\assignments\assn4\img\REGISTER.png">
Put your code and screenshots here, with proper heading organization. You don't need to include html/php code (or testing) for any pages that aren't affected by your javascript for this assignment.
