﻿using System;
using AzureTokenMaker.Service;

namespace AzureTokenMaker.App {

    public enum ProfileType {
        Client,
        User
    }

    sealed class Profile : IEquatable<Profile>{

        public string Name { get; set; }

        public TokenParameters Data {
            get; set; }

        public ProfileType Type {
            get; set; }

        public bool Equals(Profile other) {
            if (other == null) {
                return false;
            }

            if (ReferenceEquals(other, this)) {
                return true;
            }

            if (!String.IsNullOrWhiteSpace(Name) && Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase)) {
                return true;
            }

            return false;
        }

        public override bool Equals ( object obj ) {
            var target = obj as Profile;
            return Equals(target);
        }

        public override int GetHashCode () {
            return Name != null ? Name.ToLower().GetHashCode() : 0;
        }
    }
}