package test.app.one

class HealthController {
    def index() { 
        sleep(2000) {
            render(contentType: "application/json", text: "{\"healthy\":\"false\", \"version\":\"two\"}")                
        }
        render(contentType: "application/json", text: "{\"healthy\":\"false\", \"version\":\"two\"}")                
    }
}
