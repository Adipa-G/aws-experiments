package test.app.one

class HealthController {
    String appName = System.getenv("APP_NAME")
    def index() { 
        render(contentType: "application/json", text: "{\"healthy\":\"false\", \"app-name\":\"" + appName + "\"}")                
    }
}
