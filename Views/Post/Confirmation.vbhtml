@Code
    ViewData("Title") = "Job Postings"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Confirmation</title>
</head>
<body>
    <div Class="container center">
        <div class='alert alert-success'>
            Thank you for submitting your job posting!
        </div>
        <a class="btn btn-primary" href='@Url.Action("Index", "Post")'>Submit New Job</a>   
    </div>
</body>
</html>
