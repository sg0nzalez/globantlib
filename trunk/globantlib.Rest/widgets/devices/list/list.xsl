<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="ArrayOfDeviceType">

      <h2>Devices</h2>
      <ul id="w-devices-list">
        <xsl:for-each select="DeviceType">
          <li class="device">
            <a class="thumbnail" href="#devices/calendar/{ID}">
              <img width="100" src="img/{Image}" />
            </a>
            <h2 class="title">
              <a href="#devices/calendar/{ID}">
                <xsl:value-of select="Type" />
              </a>
            </h2>
            <h3 class="tagline">
              <xsl:if test="Publisher != ''">
                <span>
                  <xsl:value-of select="Publisher" />
                </span>
              </xsl:if>
              <xsl:if test="Author != ''">
                <span>
                  <xsl:value-of select="Author" />
                </span>
              </xsl:if>
              <xsl:if test="Released != ''">
                <span>
                  <xsl:value-of select="Released" />
                </span>
              </xsl:if>
            </h3>
            <ul>
              <li>
                <b>Next Date Available</b>: <xsl:value-of select="Available"/>
              </li>
              <li>
                <b>Quantity Available</b>: <xsl:value-of select="Quantity"/>
              </li>
            </ul>
            <p class="description">
              <a class="view-calendar" href="#devices/calendar/{ID}">View booking calendar</a>
            </p>
          </li>
        </xsl:for-each>
      </ul>
    
  </xsl:template>
</xsl:stylesheet>