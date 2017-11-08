## Custom login page

A lot of Episerver solutions will eventually let its users register and log in, or at least provide their editors with a branded login page.
The template includes an unstyled login-form with localization enabled, backed by .Net Membership.

Login and logout routes are mapped to AccountController, and the forms authentication-element in web.config has been set to point to the login-route when the site issues a 401 Unauthorized challenge, typically by using the [Authorize]-attribute on a controller or method.

The controller's job is setting the correct language, based on the returnUrl-parameter, showing the login form and collecting information from the user.
An interface named IUserAuthenticationHandler is injected to take care of the actual authentication.

A .Net membership implementation is included, and its implementation of the interface is wired up in WebRegistry.

### Changing the login url
If a specific url for the login forms is wanted, it is easy to change it.

- In Configurations/Web.config, find the forms-element and change the loginUrl-parameter to your desired url.
- In MvcRoutesInitializer, change the url-argument of the login-route to the same url.

### Switching User Provider
If the project uses a different provider, e.g. ASP.Net Identity, the easiest way to integrate with it is to create a new implementation of the interface IUserAuthenticationHandler that works with your chosen provider and replace the existing implementation in WebRegistry.

For SSO-style authentication, refer to the next section on reverting to the normal episerver login, as it is easier to follow from a clean slate.

### Reverting to the normal episerver login

The easiest way to revert to the original is to change this line in Configurations/Web.config:

```xml
<forms name=".EPiServerLogin" loginUrl="login" timeout="120" defaultUrl="~/" />
```

back to

```xml
<forms name=".EPiServerLogin" loginUrl="Util/Login.aspx" timeout="120" defaultUrl="~/" />
```

To completely remove all traces, remove this as well:

- The two routes in MvcRoutesInitializer 
- The custom route constants Login and Logout in ApplicationConstants.CustomRoutes
- AccountController.cs
- The folder Account in Views
- The folder UserHandling in ~/Business
- The Registry entry connecting the UserHandler-interface with its implementation in WebRegistry
- The Account section in the translations.[language].xml files
- LoginViewModel.cs