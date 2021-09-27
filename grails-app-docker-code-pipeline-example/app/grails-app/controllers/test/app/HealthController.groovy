package test.app

class HealthController {
    String appName = System.getenv("APP_NAME")
    String appVersion = System.getenv("APP_VERSION")
    def index() { 
        render(contentType: "application/json", text: "{\"healthy\":\"false\", \"app-name\":\"" + appName + "\", \"app-version\":\"" + appVersion + "\"}")                
    }
}

