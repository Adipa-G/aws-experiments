package test.app.two

class HealthController {
    def index() { 
        render(contentType: "application/json", text: "{\"healthy\":\"false\", \"app-name\":\"two\"}")                
    }
}