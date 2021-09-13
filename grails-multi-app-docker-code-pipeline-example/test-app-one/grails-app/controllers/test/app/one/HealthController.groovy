package test.app.one

class HealthController {
    def index() { 
        render(contentType: "application/json", text: "{\"healthy\":\"false\", \"version\":\"one\"}")                
    }
}
