var home = function ($scope, $http, $location) {
    $scope.obj = {};
    $scope.QuestionList = [];
    $scope.AnsList = [];
    var myEditor;
    ClassicEditor
        .create(document.querySelector('#editor'))
        .then(editor => {
            editor.ui.view.editable.element.style.height = '300px';
            //console.log('Editor was initialized', editor);
            myEditor = editor;
        })
        .catch(err => {
            console.error(err.stack);
        });
    //ClassicEditor
    //    .create(document.querySelector('#editor'))
    //    .catch(error => {
    //        console.error(error);
    //    });
    $scope.getType = function () {
        debugger;
        let x = $scope.obj.type;
        if ($scope.obj.type == "MCQ") {

            $('#mcq').removeClass('d-none');
        }
        else {
            $('#mcq').addClass('d-none');
        }
        if ($scope.obj.type == "Dropdown choice" || $scope.obj.type == "MCQ") {
            $('#choise').removeClass('d-none');
        }
        else {
            $('#choise').addClass('d-none');
        }
    }
    $scope.addRow = function () {
        debugger;
        var table = document.getElementById("myTable");
        var row = table.insertRow(0);
        var cell1 = row.insertCell(0);
        cell1.innerHTML = "<input type='text' class='form-control' id='choises' />";
    }

    $scope.Question = function () {
        debugger;
        var data = {};
        data = $scope.obj;
        var l = document.querySelectorAll("input[id=choises]").length;
        //Initialize default array
        data.choises = [];
        for (i = 0; i < l-1; i++) {
            //Push each element to the array
            data.choises.push(document.querySelectorAll("input[id=choises]")[i].value);
            //data.choises.push($('#choises').eq(i).val());
        }
        debugger;
        if (data.id != undefined) {
            $scope.updateQuestion(data);
        }
        else {
            $scope.addQuestion(data);
        }

    }
    $scope.addQuestion = function (data) {
        debugger;
        $http({
            method: 'POST',
            url: '/api/Home/createQuestion',
            data: data
        }).then(function (response) {
            debugger;
            if (response.status == 200) {
                alert("Created..");
                $scope.obj = {};
                $scope.QuestionList = [];
                //$scope.ProgrameTitle = "";
                $scope.reset();
                $scope.getQuestions();
            }
        }, function (error) {
            alert(error);
        });


    }
    $scope.submitApplication = function (id) {
        debugger;
        var data = {};
        //data.Questions = $scope.QuestionList;
        data.Id = id;
        data.ProgrameTitle = $scope.ProgrameTitle;
        //data.ProgrameDescription = $('.ck-editor__editable').html();
        
        data.ProgrameDescription = myEditor.getData();
        if (id != undefined) {
            //$scope.updateQuestion(data);
        }
        else {
            $scope.createApplication(data);
        }

    }
    $scope.submitApplicationForm = function () {
        debugger;
        let data = $scope.obj;
        data.questionAns = [];
        for (var i = 0; i < $scope.model.length; i++) {
            var ans = {};
            ans.id = $scope.model[i].id;
            if ($scope.model[i].type == 'Yes/No') {
                ans.answers = $("input[type='radio'][name='age']:checked").val();
            }
            else {
                ans.answers = document.getElementById($scope.model[i].id).value;
            }
            
            data.questionAns.push(ans);
        }
        //data.questionAns = $scope.AnsList;
        $http({
            method: 'POST',
            url: '/api/Home/submitApplicationForm',
            data: data
        }).then(function (response) {
            debugger;
            if (response.status == 200) {
                alert("Created..");
                $scope.obj = {};
                
                $scope.getQuestions();
            }
        }, function (error) {
            alert(error);
        });
    }
    $scope.addAns = function (id,ans) {
        debugger;
        var data = {};
        data.id = id;
        data.answers = document.getElementById(id).value;
        $scope.AnsList.push(data);
    }
    $scope.createApplication = function (data) {
        debugger;
        $http({
            method: 'POST',
            url: '/api/Home/createApplication',
            data: data
        }).then(function (response) {
            debugger;
            if (response.status == 200) {
                alert("Created..");
                $scope.obj = {};
                $scope.QuestionList = [];
                $scope.ProgrameTitle = "";
                $scope.getQuestions();
            }
        }, function (error) {
            alert(error);
        });

    }
    $scope.updateQuestion = function (data) {
        debugger;
        $http({
            method: 'PUT',
            url: '/api/Home/updateQuestion',
            data: data
        }).then(function (response) {
            debugger;
            if (response.status == 200) {
                alert("Created..");
                $scope.obj = {};
                $scope.QuestionList = [];
                $scope.ProgrameTitle = "";
                $scope.getQuestions();
            }
        }, function (error) {
            alert(error);
        });
    }
    $scope.update = function (id, type) {
        debugger;
        $scope.obj = {};
        $('#delete').removeClass('d-none');
        $scope.obj = $scope.model.find(user => user.id === id);
        if ($scope.obj.type == "MCQ") {

            $('#mcq').removeClass('d-none');
        }
        else {
            $('#mcq').addClass('d-none');
        }
        if ($scope.obj.type == "Dropdown choice" || $scope.obj.type == "MCQ") {
            for (var i = 0; i < $scope.obj.choises.length; i++) {
                var table = document.getElementById("myTable");
                var row = table.insertRow(0);
                var cell1 = row.insertCell(0);
                cell1.innerHTML = "<input type='text' class='form-control'  id='choises' value='" + $scope.obj.choises[i] +"'/>";
            }
            $('#choise').removeClass('d-none');
        }
        else {
            $('#choise').addClass('d-none');
        }

        document.getElementById('questionId').value = id;
        document.getElementById('mode_show').click();

    }
    $scope.deleteQuestion = function (id,type) {
        debugger;
        $http({
            method: 'DELETE',
            url: '/api/Home/deleteQuestion?id=' + id + '&type=' + type
        }).then(function (response) {
            debugger;
            console.log(response);
            document.getElementById('mode_show').click();
            $scope.model = response.data.result;
            $scope.obj = {};
            $scope.QuestionList = [];
            $scope.ProgrameTitle = "";
            $scope.getQuestions();
            //$scope.ProgrameTitle = $scope.model.programeTitle
        }, function (error) {
            alert(error);
        });
        
    }
    $scope.reset = function () {
        debugger;
        $scope.obj = {};
        $('#delete').addClass('d-none');
        $('#choise').addClass('d-none');
        $('#mcq').addClass('d-none');
        var table = document.getElementById("myTable").innerHTML = "";

    }
    $scope.checkselect = function () {
        debugger;
        var l = $("select option:selected").length;
        if ($("select option:selected").length > 3) {
            //your code here
        }

    }
    $scope.getQuestions = function () {
        debugger;
        $http({
            method: 'GET',
            url: '/api/Home/getQuestions'
        }).then(function (response) {
            debugger;
            console.log(response);
            $scope.model = response.data.result;
            //$scope.ProgrameTitle = $scope.model.programeTitle
        }, function (error) {
            alert(error);
        });
    }
    $scope.getQuestions();
    $scope.getForm = function () {
        debugger;
        $http({
            method: 'GET',
            url: '/api/Home/getForm'
        }).then(function (response) {
            debugger;
            console.log(response);
            $scope.modelform = response.data.result[0]; 
            $scope.ProgrameTitle = $scope.modelform.programeTitle;
            $scope.ProgrameDescription = $scope.modelform.programeDescription;
            document.getElementById('renderHtml').innerHTML = $scope.modelform.programeDescription;
        }, function (error) {
            alert(error);
        });
    }
    $scope.getForm();
    
}

