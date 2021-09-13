package test.app.one

import java.util.Random

class LoadController {
    def index() { 
        int nThrows = 0
        int nSuccess = 0
        double x, y
        long then = System.nanoTime()
        int events=1e8
        Random r = new Random()
        for (int i = 0; i < events; i++) {
                    x = r.nextFloat()      // Throw a dart
                    y = r.nextFloat()
                    nThrows++
                    if ( x*x + y*y <= 1 )  nSuccess++
        }
        double itime = ((System.nanoTime() - then)/1e9)
        
        render(contentType: "application/json", text: "{\"time_to_calc\":\"" + itime + "\",\"pi\":\"" + 4*(double)nSuccess/(double)nThrows + "\"}")                
    }
}
