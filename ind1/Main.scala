import swing.{Panel, MainFrame, SimpleSwingApplication}
import java.awt.{Color, Graphics2D, Dimension, Font, BasicStroke}
import java.awt.image.BufferedImage
import java.awt.geom._

class DataPanel extends Panel {

  def drawEdge(g: Graphics2D, e: Edge) {
    g.draw(new Line2D.Double(e.a._1, e.a._2, 
                             e.b._1, e.b._2))
  }

  def drawPolygon(g: Graphics2D, p: Polygon) {

    g.setStroke(new BasicStroke())  // reset to default
    g.setColor(new Color(0, 0, 255)) // same as Color.BLUE
    p.edges map (e => drawEdge(g, e))
  }


  override def paintComponent(g: Graphics2D) {
    val size = Config.WindowSize


    val canvas = new BufferedImage(size._1, size._2, BufferedImage.TYPE_INT_RGB)

    g.setColor(Color.WHITE)
    g.fillRect(0, 0, canvas.getWidth, canvas.getHeight)


    val customPolygon = true
    var points: List[(Float, Float)] = List[(Float, Float)]()

    if (customPolygon) {
      // // Square
      // points = List[(Float, Float)](
      //   (10, 10), 
      //   (110, 10), 
      //   (110, 110), 
      //   (10, 110)
      //   )
      points = List[(Float, Float)](
        (10, 10), 
        (110, 10), 
        (210, 110), 
        (310, 210), 
        (360, 260), 
        (410, 410),
        (310, 510),
        (100, 310),
        (50, 210)
        )
    } else {
      val n = 20

      points = (for {i <- 0 until n} yield 
                  ((i * Config.WindowSize._1 / n).toFloat, 
                   (Math.random() * Config.WindowSize._2).toFloat)).toList
    }

    val triangles = Delaunay.triangulate(points)


    g.setStroke(new BasicStroke(5))  
    g.setColor(new Color(255, 0, 0)) 

    for {i <- 1 until points.length} yield drawEdge(g, Edge(points(i - 1), points(i)))
    drawEdge(g, Edge(points(0), points(points.length - 1)))

    
    g.setStroke(new BasicStroke())  
    g.setColor(new Color(0, 0, 255)) 

    triangles map { t => {
        drawEdge(g, Edge((points(t._1)._1, points(t._1)._2),
                         (points(t._2)._1, points(t._2)._2)));
        drawEdge(g, Edge((points(t._3)._1, points(t._3)._2),
                         (points(t._2)._1, points(t._2)._2)));
        drawEdge(g, Edge((points(t._1)._1, points(t._1)._2),
                         (points(t._3)._1, points(t._3)._2)));
      }
    }



    g.dispose()
  }
}



object Draw extends SimpleSwingApplication {


  def top = new MainFrame {
    contents = new DataPanel {
      preferredSize = new Dimension(Config.WindowSize._1, Config.WindowSize._2)
    }
  }
}
