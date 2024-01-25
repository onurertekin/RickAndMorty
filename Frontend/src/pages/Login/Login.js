import React, { useEffect, useState } from "react";
import "./Login.scss";

const Login = () => {
    let [username, setUsername] = React.useState("");
    let [password, setPassword] = React.useState("");
  
    useEffect(() => {

    }, []);

    const loginOnClick = async () => {
        
        await fetch(
            "http://localhost:5016/iam/authentication",
            {
              method: "POST",
              headers: {
                "Content-Type": "application/json"
              },
              body: JSON.stringify({
                username: username,
                password: password,
              }),
            }
          ).then(function(response) {
            return response.json();
          }).then(function(data) {
           
            if(data.token != undefined){
               localStorage.setItem("token", data.token);
               window.location.href = "/";
            }
            
          });
    };

    return (
        <div>
            
            {/* <input type="text" value={username} />
            <input type="text" value={password} /> */}

            <input type="text" required onChange={(e)=>setUsername(e.target.value)} placeholder='Username'/>
               
            <input type="password" required onChange={(e)=>setPassword(e.target.value)} placeholder='Please enter a strong password'/>

            <input type="button" value="Login" onClick={loginOnClick} />
        </div>
    );
};


export default Login;
