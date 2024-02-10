Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports TestMVCvb1

Namespace Controllers
    Public Class ContactController
        Inherits System.Web.Mvc.Controller

        Private db As New TESTEntities

        ' GET: Contact
        Function Index() As ActionResult
            Return View(db.TB_Contact.ToList())
        End Function

        ' GET: Contact/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim tB_Contact As TB_Contact = db.TB_Contact.Find(id)
            If IsNothing(tB_Contact) Then
                Return HttpNotFound()
            End If
            Return View(tB_Contact)
        End Function

        ' GET: Contact/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Contact/Create
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        'más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ContactID,Name,Surname,Surname2,Telephone,e_mail")> ByVal tB_Contact As TB_Contact) As ActionResult
            If ModelState.IsValid Then
                db.TB_Contact.Add(tB_Contact)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(tB_Contact)
        End Function

        ' GET: Contact/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim tB_Contact As TB_Contact = db.TB_Contact.Find(id)
            If IsNothing(tB_Contact) Then
                Return HttpNotFound()
            End If
            Return View(tB_Contact)
        End Function

        ' POST: Contact/Edit/5
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        'más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ContactID,Name,Surname,Surname2,Telephone,e_mail")> ByVal tB_Contact As TB_Contact) As ActionResult
            If ModelState.IsValid Then
                db.Entry(tB_Contact).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(tB_Contact)
        End Function

        ' GET: Contact/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim tB_Contact As TB_Contact = db.TB_Contact.Find(id)
            If IsNothing(tB_Contact) Then
                Return HttpNotFound()
            End If
            Return View(tB_Contact)
        End Function

        ' POST: Contact/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim tB_Contact As TB_Contact = db.TB_Contact.Find(id)
            db.TB_Contact.Remove(tB_Contact)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
