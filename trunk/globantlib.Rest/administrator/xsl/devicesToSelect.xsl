<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">


  <xsl:template match="ArrayOfDeviceType">
    <option val="">Select one...</option>
    <xsl:for-each select="DeviceType">

      <option value="{ID}">
        <xsl:value-of select="Type"/>
      </option>

    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
