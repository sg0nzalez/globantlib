<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:template match="Calendar">
    <ul id="w-calendar-items">
      <xsl:for-each select="Type/Item">
        <li>
          <xsl:choose>
            <xsl:when test="Current = 'Yes'">
              <a class="current" href="#{ID}">
                <xsl:value-of select="Type"/>
                <xsl:text> </xsl:text>
                <xsl:value-of select="ID"/>
              </a>
            </xsl:when>
            <xsl:otherwise>
              <a href="#{ID}">
                <xsl:value-of select="Type"/>
                <xsl:text> </xsl:text>
                <xsl:value-of select="ID"/>
              </a>
            </xsl:otherwise>          
          </xsl:choose>
        </li>
      </xsl:for-each>
    </ul>
    <p id="w-calendar-month-picker">
        <a href="#" class="prev" title="Previous Month">Previous Month</a>
        <a href="#" class="next" title="Next Month">Next Month</a>
        <span class="month-name"></span>
    </p>
    <div id="w-calendar-entries">
      <xsl:for-each select="Type/Item">
        <xsl:if test="Current = 'Yes'">
          <xsl:for-each select="Leases/Month">
          <div class="month">
            <h3>
              <xsl:value-of select="Name"/>
            </h3>
            <xsl:for-each select="Date">

              <xsl:choose>
                <xsl:when test="Username != ''">
                  <div class="date">
                    <span class="number">
                      <xsl:value-of select="Number"/>
                    </span>
                      <xsl:value-of select="Username"/>
                  </div>
                </xsl:when>
                <xsl:otherwise>
                  <div class="date free">
                    <span class="number">
                      <xsl:value-of select="Number"/>
                    </span>
                  </div>
                </xsl:otherwise>
              </xsl:choose>
              
              
            </xsl:for-each>
            </div>
          </xsl:for-each>
        </xsl:if>
      </xsl:for-each>
    </div>
  </xsl:template>
  
</xsl:stylesheet>