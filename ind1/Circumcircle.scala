
class Circumcircle(p1:(Float, Float),
                          p2:(Float, Float), 
                          p3:(Float, Float)) {
  if (Math.abs(p1._2 - p2._2) < Config.EPS && Math.abs(p2._2 - p3._2) < Config.EPS) {
    System.err.println("Circumcircle: Points are colinear");
  }
  private val mid1 = ((p1._1 + p2._1)/2, (p1._2 + p2._2)/2)
  private val mid2 = ((p2._1 + p3._1)/2, (p2._2 + p3._2)/2)
  
  val center = 
    if (Math.abs(p2._2 - p1._2) < Config.EPS) {
      val d2 = -(p3._1 - p2._1) / (p3._2 - p2._2)
      val xc =  mid1._1
      val yc =  d2 * (xc - mid2._1) + mid2._2
      (xc, yc)
    } else {
      if (Math.abs(p3._2 - p2._2) < Config.EPS) {
        val d1 = -(p2._1 - p1._1) / (p2._2 - p1._2)
        val xc =  mid2._1
        val yc =  d1 * (xc - mid1._1) + mid1._2
        (xc, yc)
      } else {
        val d1 = -(p2._1 - p1._1) / (p2._2 - p1._2)
        val d2 = -(p3._1 - p2._1) / (p3._2 - p2._2)
        val xc =  ((d1 * mid1._1 - mid1._2) - (d2 * mid2._1 - mid2._2)) / (d1 - d2)
        val yc =  d1 * (xc - mid1._1) + mid1._2
        (xc, yc)
      }
    }
    
  val radiusSquared = {
    val (dx, dy) = (p2._1 - center._1, p2._2 - center._2) 
    dx*dx + dy*dy
  }

  val radius = Math.sqrt(radiusSquared).toFloat

  def inside(point: (Float, Float)): Boolean = {
    val (dx, dy) = (point._1 - center._1, point._2 - center._2)
    val qsqr = dx*dx + dy*dy
    (qsqr <= radiusSquared)
  }
}
