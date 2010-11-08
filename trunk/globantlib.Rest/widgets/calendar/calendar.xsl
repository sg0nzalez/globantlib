<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:template match="ArrayOfTypes">
    <h2>Click on the calendar to make a reservation.</h2>
    <ul id="w-calendar-items">
      <xsl:for-each select="Types/Items/Item">
        <li>
          <xsl:choose>
            <xsl:when test="Current = 'true'">
              <a deviceid="{ID}" class="current" href="#{ID}">
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
      <p id="w-calendar-helper">Select ending date <a class="calendar-reset" href="">Cancel</a></p>
      <form id="w-calendar-form">
        <input id="w-calendar-email" />
      </form>
      <xsl:for-each select="Types/Items/Item">
        <xsl:if test="Current = 'true'">
          <xsl:for-each select="Lease/Month">
            <h3>
              <xsl:value-of select="Name"/>
            </h3>
            <xsl:for-each select="Dates/Date">

              <xsl:choose>
                <xsl:when test="Username != ''">
                  <div class="date used">
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
          </xsl:for-each>
        </xsl:if>
      </xsl:for-each>
    </div>
  </xsl:template>
  
</xsl:stylesheet>