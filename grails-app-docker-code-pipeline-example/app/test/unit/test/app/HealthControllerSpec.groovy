package test.app

import grails.test.mixin.TestFor
import spock.lang.Specification

/**
 * See the API for {@link grails.test.mixin.web.ControllerUnitTestMixin} for usage instructions
 */
@TestFor(HealthController)
class HealthControllerSpec extends Specification {

    def setup() {
    }

    def cleanup() {
    }

    void "app health controller"() {
        when:
        def x = 1
        
        then:
        1 == 1
    }
}
