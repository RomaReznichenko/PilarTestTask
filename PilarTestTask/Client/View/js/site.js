var depatment = [];
var user = [];
var mandatoryInformationId = [];
var flag = false; 
var test = [{"id": 2, "name": "2"}, {"id": 1002, "name": "1002"}];

 function Hide(){ // лoвим клик пo крестику или пoдлoжке
    $('#modal_form')
      .animate({opacity: 0, top: '45%'}, 200,  // плaвнo меняем прoзрaчнoсть нa 0 и oднoвременнo двигaем oкнo вверх
        function(){ // пoсле aнимaции
          $(this).css('display', 'none'); // делaем ему display: none;
          $('#overlay').fadeOut(400); // скрывaем пoдлoжку
        }
      );
  }

    $(document).ready(function() { // вся мaгия пoсле зaгрузки стрaницы
 // лoвим клик пo ссылки с id="go"
    event.preventDefault(); // выключaем стaндaртную рoль элементa
    $('#overlay').fadeIn(400, // снaчaлa плaвнo пoкaзывaем темную пoдлoжку
      function(){ // пoсле выпoлнения предъидущей aнимaции
        $('#modal_form') 
          .css('display', 'block') // убирaем у мoдaльнoгo oкнa display: none;
          .animate({opacity: 1, top: '50%'}, 200); // плaвнo прибaвляем прoзрaчнoсть oднoвременнo сo съезжaнием вниз
    });
});
    function BtnLoginClick(){
      var login = document.getElementById("login").value;
      var password = document.getElementById("password").value;
      var identInf = JSON.parse(Identification(login, password));
      var id = -1;
      if(identInf.length > 0)
      {
        if (identInf[0]["isAdmin"] == 1){
          flag = true;
          MandatoryInformation();
          GetMandatory();
          Municipality();
          Busines();
        }
        else{
            id = identInf[0]["id"];
        }

        Hide();
        GetDepatment();
        Contact(id);
        GetUser();
        User(id);
        Depatment(id);
      }
      else{
        alert("Wrong login or password!");
      }
    }

    function Identification(name, password){
      var ansver;
      var xhr = new XMLHttpRequest();
      xhr.open('POST', "http://localhost:49722/User/Identification", false);
      xhr.setRequestHeader("Content-Type", "application/json");

      xhr.onreadystatechange = function () {
          if (xhr.readyState == 4 && xhr.status == 200) {
              var response = xhr.responseText;
              if (response) {
                  ansver = response;
               }
          }
      }
      xhr.send(JSON.stringify({ name: name, password: password }));
      return ansver;
    }


function GetDepatment() {
    var grid = this._grid;
    var xmlhttp = new XMLHttpRequest();
    var url = "http://localhost:49722//Depatment";
    xmlhttp.open("GET", url, false);
    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var responseJson = JSON.parse(this.responseText);
            for (var i = 0; i < responseJson.length; i++) {
                var item = new Object();
                item.id = responseJson[i].id;
                item.name = responseJson[i].name;
                item.addres = responseJson[i].address;
                item.userId = responseJson[i].userId;
                item.mandatoryId = responseJson[i].mandatoryInformationId;
                depatment.push(item);
            }
        }
    };
    xmlhttp.send();
}

function GetUser() {
    var grid = this._grid;
    var xmlhttp = new XMLHttpRequest();
    var url = "http://localhost:49722//User";
    xmlhttp.open("GET", url, false);
    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var responseJson = JSON.parse(this.responseText);
            for (var i = 0; i < responseJson.length; i++) {
                var item = new Object();
                item.id = responseJson[i].id;
                item.name = responseJson[i].name;
                user.push(item);
            }
        }
    };
    xmlhttp.send();
}

function GetMandatory() {
    var grid = this._grid;
    var xmlhttp = new XMLHttpRequest();
    var url = "http://localhost:49722//MandatoryInformation";
    xmlhttp.open("GET", url, false);
    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var responseJson = JSON.parse(this.responseText);
            for (var i = 0; i < responseJson.length; i++) {
                var item = new Object();
                item.id = responseJson[i].id;
                item.name = responseJson[i].name;
                mandatoryInformationId.push(item);
            }
        }
    };
    xmlhttp.send();
}

function MandatoryInformation () {
$.ajax({
        type: "GET",
        url: "http://localhost:49722/MandatoryInformation"
    }).done(function () {
        $("#jsGridMandatoryInformation").jsGrid({
            width: "100%",
            height: "550px",
            inserting: flag,
            editing: true,
            sorting: false,
            paging: false,
            autoload: true,
            pageSize: 10,
            controller: {
                
                loadData: function (filter) {
                    return $.ajax({
                        type: "GET",
                        url: "http://localhost:49722/MandatoryInformation",
                        data: filter
                    });
                },
                insertItem: function (item) {
                    item.id = 0;
                    return $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "http://localhost:49722/MandatoryInformation/add",
                        data: JSON.stringify(item)
                    });
                },
                updateItem: function (item) {
                    return $.ajax({
                        type: "PUT",
                        contentType: "application/json",
                        url: "http://localhost:49722/MandatoryInformation/update",
                        data: JSON.stringify(item)
                    });
                },
                deleteItem: function (item) {
                    return $.ajax({
                        type: "DELETE",
                        contentType: "application/json",
                        url: "http://localhost:49722/MandatoryInformation/delete",
                        data: JSON.stringify(item)
                    });
                }
            },
            fields: [
              { name: "name", type: "text", width: 80, validate: "required" },
              { name: "addres", type: "text", width: 80 },
              { name: "email", type: "text", width: 80 },
              { name: "phone", type: "text", width: 80 },
              { name: "comments", type: "textarea", width: 250 },
              { type: "control", deleteButton: flag, editButton: flag}
          ]
        });
    });

}

function Contact (id) {
$.ajax({
        type: "GET",
        url: "http://localhost:49722/Contact"
    }).done(function () {
        $("#jsGridContact").jsGrid({
            width: "100%",
            height: "550px",
            inserting: flag,
            editing: flag,
            sorting: false,
            paging: false,
            autoload: true,
            pageSize: 10,
            controller: {
                loadData: function (filter) {
                    if(id < 0){
                        return $.ajax({
                            type: "GET",
                            url: "http://localhost:49722/Contact",
                            data: filter
                        });
                    }
                    else{
                        return $.ajax({
                            type: "POST",
                            contentType: "application/json",
                            url: "http://localhost:49722/Contact/GetContact",
                            data: JSON.stringify(id)
                        });
                    }                    
                },
                insertItem: function (item) {
                    item.id = 0;
                    return $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "http://localhost:49722/Contact/add",
                        data: JSON.stringify(item)
                    });
                },
                updateItem: function (item) {
                    return $.ajax({
                        type: "PUT",
                        contentType: "application/json",
                        url: "http://localhost:49722/Contact/update",
                        data: JSON.stringify(item)
                    });
                },
                deleteItem: function (item) {
                    return $.ajax({
                        type: "DELETE",
                        contentType: "application/json",
                        url: "http://localhost:49722/Contact/delete",
                        data: JSON.stringify(item)
                    });
                }
            },
            fields: [
              { name: "name", type: "text", width: 80, validate: "required" },
              { name: "role", type: "text", width: 80 },
              { name: "phone", type: "text", width: 80 },
              { name: "mail", type: "text", width: 80 },
              { type: "control", deleteButton: flag, editButton: flag }
          ]
        });

    });
}

function User (id) {
$.ajax({
        type: "GET",
        url: "http://localhost:49722/User"
    }).done(function () {
        $("#jsGridUser").jsGrid({
            width: "100%",
            height: "550px",
            inserting: flag,
            editing: flag,
            sorting: false,
            paging: false,
            autoload: true,
            pageSize: 10,
            controller: {
                loadData: function (filter) {
                    if(id < 0){
                        return $.ajax({
                            type: "GET",
                            url: "http://localhost:49722/User",
                            data: filter
                        });
                    }
                    else{
                        return $.ajax({
                            type: "POST",
                            contentType: "application/json",
                            url: "http://localhost:49722/User/GetUser",
                            data: JSON.stringify(id)
                        });
                    }
                },
                insertItem: function (item) {
                    item.id = 0;
                    return $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "http://localhost:49722/User/add",
                        data: JSON.stringify(item)
                    });
                },
                updateItem: function (item) {
                    return $.ajax({
                        type: "PUT",
                        contentType: "application/json",
                        url: "http://localhost:49722/User/update",
                        data: JSON.stringify(item)
                    });
                },
                deleteItem: function (item) {
                    return $.ajax({
                        type: "DELETE",
                        contentType: "application/json",
                        url: "http://localhost:49722/User/delete",
                        data: JSON.stringify(item)
                    });
                }
            },
            fields: [
              { name: "name", type: "text", width: 80, validate: "required" },
              { name: "mobile", type: "text", width: 80 },
              { name: "mail", type: "text", width: 80 },              
              { name: "depatmentId", type: "select", items: depatment, valueField: "id", textField: "name" },
              { name: "userName", type: "text", width: 80 },
              { type: "control", deleteButton: flag, editButton: flag }
          ]
        });

    });
}

function Depatment (id) {
$.ajax({
        type: "GET",
        url: "http://localhost:49722/Depatment"
    }).done(function () {
        $("#jsGridDepartment").jsGrid({
            width: "100%",
            height: "550px",
            inserting: flag,
            editing: flag,
            sorting: false,
            paging: false,
            autoload: true,
            pageSize: 10,
            controller: {
                loadData: function (filter) {
                    if(id < 0){
                        return $.ajax({
                            type: "GET",
                            url: "http://localhost:49722/Depatment",
                            data: filter
                        });
                    }
                    else{
                        return $.ajax({
                            type: "POST",
                            contentType: "application/json",
                            url: "http://localhost:49722/Depatment/GetDepatment",
                            data: JSON.stringify(id)
                        });
                    }
                },
                insertItem: function (item) {
                    item.id = 0;
                    return $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "http://localhost:49722/Depatment/add",
                        data: JSON.stringify(item)
                    });
                },
                updateItem: function (item) {
                    return $.ajax({
                        type: "PUT",
                        contentType: "application/json",
                        url: "http://localhost:49722/Depatment/update",
                        data: JSON.stringify(item)
                    });
                },
                deleteItem: function (item) {
                    return $.ajax({
                        type: "DELETE",
                        contentType: "application/json",
                        url: "http://localhost:49722/Depatment/delete",
                        data: JSON.stringify(item)
                    });
                }
            },
            fields: [
              { name: "name", type: "text", width: 80, validate: "required" },             
              { name: "userId", type: "select", items: user, valueField: "id", textField: "name" },
              { name: "address", type: "text", width: 80 },
              { type: "control", deleteButton: flag, editButton: flag }
          ]
        });

    });
}

function Municipality () {
$.ajax({
        type: "GET",
        url: "http://localhost:49722/Municipality"
    }).done(function () {
        $("#jsGridMunicipality").jsGrid({
            width: "100%",
            height: "550px",
            inserting: false,
            editing: flag,
            sorting: false,
            paging: false,
            autoload: true,
            pageSize: 10,
            controller: {
                loadData: function (filter) {
                    return $.ajax({
                        type: "GET",
                        url: "http://localhost:49722/Municipality",
                        data: filter
                    });                   
                },
                updateItem: function (item) {
                    return $.ajax({
                        type: "PUT",
                        contentType: "application/json",
                        url: "http://localhost:49722/Municipality/update",
                        data: JSON.stringify(item)
                    });
                },
            },
            fields: [             
              { name: "mandatoryInformationId", type: "select", items: mandatoryInformationId, valueField: "id", textField: "name" },
              { name: "numberOfSchool", type: "text", width: 80 },
              { type: "control", deleteButton: false, editButton: flag }
          ]
        });

    });
}

function Busines () {
$.ajax({
        type: "GET",
        url: "http://localhost:49722/Business"
    }).done(function () {
        $("#jsGridBusines").jsGrid({
            width: "100%",
            height: "550px",
            inserting: false,
            editing: flag,
            sorting: false,
            paging: false,
            autoload: true,
            pageSize: 10,
            controller: {
                loadData: function (filter) {
                    return $.ajax({
                        type: "GET",
                        url: "http://localhost:49722/Business",
                        data: filter
                    });                   
                },
                updateItem: function (item) {
                    return $.ajax({
                        type: "PUT",
                        contentType: "application/json",
                        url: "http://localhost:49722/Business/update",
                        data: JSON.stringify(item)
                    });
                },
            },
            fields: [             
              { name: "mandatoryInformationId", type: "select", items: mandatoryInformationId, valueField: "id", textField: "name" },
              { type: "control", deleteButton: false, editButton: flag }
          ]
        });

    });
}



