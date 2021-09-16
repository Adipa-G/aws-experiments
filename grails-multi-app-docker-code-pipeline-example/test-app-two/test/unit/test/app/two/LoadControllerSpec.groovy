package test.app.two

import grails.test.mixin.TestFor
import spock.lang.Specification

/**
 * See the API for {@link grails.test.mixin.web.ControllerUnitTestMixin} for usage instructions
 */
@TestFor(LoadController)
class LoadControllerSpec extends Specification {

    def setup() {
    }

    def cleanup() {
    }

    void "app two test load controller"() {
        when:
        def x = 1
        
        then:
        1 == 1
    }
}
