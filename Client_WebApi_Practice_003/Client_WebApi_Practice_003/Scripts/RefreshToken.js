//Gets the Refresh token from WebApi using existing refresh token,clientId,clientsecret and stores in the session
function RefreshToken()
{
    var refreshToken = sessionStorage["refreshtoken"];
    var uri = webApiUri + "/token";
    var user = "grant_type=refresh_token" + "&client_id=" + clientID + "&client_secret=" + clientSecret + "&refresh_token=" + refreshToken;
    $.ajax({
        url: uri,
        type:"POST",
        data: user,
        headers:{
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        success:function(response,status,e)
        {
            console.log('refresh complete');
            SetTokenInSession(response);
        },
        error:function(jqXHR,status,e)
        {
            alert(jqXHR.responseText + "---" + user);
            console.log('OnError:  ' + sessionStorage["tokenexpiration"]);
        }
    });
}

//Sets the token in the session and calls the refresh token
function SetTokenInSession(response)
{
    clearTimeout(timeOut);
    sessionStorage.clear();
    sessionStorage.setItem("token", response.access_token);
    sessionStorage.setItem("refreshtoken", response.refresh_token);
    sessionStorage.setItem("tokenexpiration", response.expires_in);
    console.log('Orig:  '+sessionStorage["tokenexpiration"]);

    //Call the RefreshToken 3 secs before the access_token expires
    CallAfterTimeOut();
}


//Call the RefreshToken 3 secs before the access_token expires
function CallAfterTimeOut()
{
    timeOut=   setTimeout(function () {
        RefreshToken();
    }, (parseInt(sessionStorage["tokenexpiration"]) - 10) * 1000);
}
 
$(function () {    
    if (sessionStorage["refreshtoken"] != null && sessionStorage["refreshtoken"] != undefined) {
        CallAfterTimeOut();
    }
});

var timeOut;