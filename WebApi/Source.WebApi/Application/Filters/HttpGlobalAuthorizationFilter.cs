using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Source.WebApi.Application.Authentication;
using Source.WebApi.Application.Exceptions;

namespace Source.WebApi.Application.Filters
{
    public class HttpGlobalAuthorizationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(":::: New Request ::::");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("-----> Headers collection...................................");
            foreach (var header in context.HttpContext.Request.Headers)
            {
                Console.WriteLine("{0}: {1}", header.Key, header.Value);
            }

            //Get X-JWT-Assertion header, with user information
            string xJwtAssertion = context.HttpContext.Request.Headers["X-JWT-Assertion"].ToString().Trim();
            //Remove X-JWT-Assertion header
            context.HttpContext.Request.Headers.Remove("X-JWT-Assertion");

            if (!string.IsNullOrEmpty(xJwtAssertion) && !string.IsNullOrWhiteSpace(xJwtAssertion))
            {
                //First, split X-JWT-Assertion header. The format of
                //JWT token is the following: {header}.{payload}[.{signature}]
                string[] xJwtAssertionEncode = xJwtAssertion.Split(
                    new string[] { "." },
                    StringSplitOptions.None);//[0].ToString() + "=";

                #region ---- Encode ----
                //Position #0: header encode Base64
                if (xJwtAssertionEncode.Length > 0)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("X-JWT-Assertion Encode Header (Position #0): {0}", xJwtAssertionEncode[0]);
                }
                //Position #1: payload encode Base64
                if (xJwtAssertionEncode.Length > 1)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("X-JWT-Assertion Encode Payload (Position #1): {0}", xJwtAssertionEncode[1]);
                }
                //Position #2: signature encode Base64 (optional)
                if (xJwtAssertionEncode.Length > 2)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("X-JWT-Assertion Encode Signature (Position #2): {0}", xJwtAssertionEncode[2]);
                }
                #endregion

                #region ---- Decode ----
                //Decode X-JWT-Assertion 
                //Position #0: header
                string xJwtAssertionDecodeHeader = string.Empty;
                if (xJwtAssertionEncode.Length > 0)
                {
                    xJwtAssertionDecodeHeader = Base64Decode(xJwtAssertionEncode[0]);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("X-JWT-Assertion Decode Header (Position #0): {0}", xJwtAssertionDecodeHeader);
                }
                //Position #1: payload
                string xJwtAssertionDecodePayload = string.Empty;
                if (xJwtAssertionEncode.Length > 1)
                {
                    xJwtAssertionDecodePayload = Base64Decode(xJwtAssertionEncode[1])
                        .Replace("\\/", "/");
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("X-JWT-Assertion Decode Payload (Position #1): {0}", xJwtAssertionDecodePayload);
                }
                //Position #2: signature
                string xJwtAssertionDecodedSignature = string.Empty;
                if (xJwtAssertionEncode.Length > 2)
                {
                    xJwtAssertionDecodedSignature = Base64Decode(xJwtAssertionEncode[2]);
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("X-JWT-Assertion Decode Signature (Position #2): {0}", xJwtAssertionDecodedSignature);
                }
                #endregion

                //Inject User Identity, get the UserIdentity from claims
                //and add to app services
                var claims = Newtonsoft.Json.Linq.JObject.Parse(xJwtAssertionDecodePayload); 
                UserIdentity userIdentity = new UserIdentity()
                {
                    Username = (string)claims["http://wso2.org/claims/enduser"],
                    //Name = (string)claims["http://wso2.org/claims/givenname"],
                    //Surname = (string)claims["http://wso2.org/claims/lastname"],
                    //Email = (string)claims["http://wso2.org/claims/emailaddress"]
                };
                userIdentity.Username = userIdentity.Username
                    .Replace("@carbon.super", "");
                
                //JArray roles = (JArray)claims["http://wso2.org/claims/role"];
                //userIdentity.Roles = roles.Select(r => (string)r).ToList();

                //Serializing UserIdentity && add X-User-Identity to Request
                context.HttpContext.Request.Headers.Add("X-User-Identity", JsonConvert.SerializeObject(userIdentity));

                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("X-User-Identity: {0}", JsonConvert.SerializeObject(userIdentity));

                //End...
                Console.WriteLine("##############################################################");
            }
            else
            {
                throw new AuthenticationException("It was not possible to obtain the identity of the user from the authorization token");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }

        protected string Base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

    }
}